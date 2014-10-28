using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Plugins;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives.S3D;
using Microsoft.AnaglyphShaders3D;

namespace Microsoft.SilverlightMediaFramework.Plugins.Anaglyph3D
{
    [ExportS3DPlugin(PluginName = "Anaglyph3DPlugin",
                    PluginDescription = "Anaglyph3DPlugin",
                    PluginVersion = "1.0")]
    public class Anaglyph3DPlugin : IS3DPlugin
    {
        #region Notes
        /*
		 Notes on the approach being used for the Anaglyph 3D implementation:		 
		 
		 The order of Silverlight UIelement compositing is:
			1.	Bitmap caching
			2.	Opacity
			3.	Shader effect
			4.	Clip
			5.	Perspective
			6.	Transform

		 * A key factor in ensuring the Anaglyph 3D pixel shaders perform well is to 
		 * make sure that the pixel shaders are applied to the native resolution video, not
		 * the scaled-up video size.  A Canvas must be used to scale up the video so
		 * that the bitmap caching/GPU acceleration can be applied to the Canvas and used to scale
		 * the video, but not pollute the SSME/ME with caching that would interfere with shading
		 * the video at the native resolution.  Given the order above, the process we'll follow will 
		 * do the following:
		 *
		 * 1.	MediaElement level:
		 *		ME decodes video at native resolution (Stretch==”None”), width and height set explicitly to that
		 *		of the native video resolution (or current bitrate's native resolution in the case of SSME).
		 * 2.	SSME level:
		 *		a. ME video frame is passed to the SSME parent but bitmap caching is skipped because SSME.EnableGPUAcceleration==False.
		 *		b. Anaglyph pixel shader effect is applied to video frame.
		 *		c. Shaded video frame is clipped to half its size (vertically or horizontally).
		 * 3.	Canvas level:
		 *		a. Clipped video frame is passed to canvas parent and bitmap cached.
		 *		b. Canvas is scaled/transformed to fill the desired video window size.
		 *		
		 * The Canvas is injected into the Visual tree by this plugin, and is named "PluginInjectedCanvas"
		 * to allow the Core SMF to detect its presence and skip its own loading of the MediaPresenterElement's
		 * content.
		 */
        #endregion

        #region Anaglyph3DPlugin Members

        /// <summary>
        /// Indicates the Anaglyph 3D display mechanism.
        /// </summary>
        [Flags]
        public enum AnaglyphDisplayMechanism
        {
            None = 0,
            AnaglyphLeftOnly = 0x1,
            AnaglyphGrayRedCyan = 0x2,
            AnaglyphHalfColorRedCyan = 0x4
        }

        /// <summary>
        /// The anaglyph 3d plugin supports either stretching the content
        /// to Uniform or to Fill.  UniformToFill and None are not supported.
        /// </summary>
        [Flags]
        public enum StretchModes
        {
            Uniform = 0,
            Fill = 0x1
        }

        private const S3D_Formats SupportedS3DFormatsInternal = S3D_Formats.SideBySide | S3D_Formats.TopAndBottom;

        private double XFactor, YFactor;
        private bool validPropertiesForAnaglyph = false;
        private bool isSmoothStream = false;
        private bool pluginIsEnabled = true;

        private SMFPlayer smfPlayer;
        private ContentControl mediaPresenterElement;
        private IMediaPlugin activeMediaPlugin;
        private Canvas canvas;
        //Default the display mechanism to half color
        private AnaglyphDisplayMechanism activeDisplayMechanism = AnaglyphDisplayMechanism.AnaglyphHalfColorRedCyan;
        private double ssmeMaxWidth = 0;
        private double ssmeMaxHeight = 0;
        private StretchModes stretchMode = StretchModes.Uniform;

        private static Anaglyph3DPlugin current;

        #endregion

        #region Anaglyph3DPlugin Properties

        /// <summary>
        /// The current instance of the Anaglyph 3D Plugin.
        /// This allows a consuming application to reference the plugin and control it.
        /// For example, a consuming application may wish to call SetActiveDisplayMechanism() on 
        /// this Anaglyphy plugin to specify Gray vs. Half color 3D display.
        /// </summary>
        public static Anaglyph3DPlugin Current
        {
            get { return current; }
        }

        public AnaglyphDisplayMechanism ActiveDisplayMechanism
        {
            get { return activeDisplayMechanism; }
            set
            {
                activeDisplayMechanism = value;
                SetEffect();
            }
        }

        public StretchModes StretchMode
        {
            get { return stretchMode; }
            set
            {
                stretchMode = value;
                RemoveVideoProperties();
                SetVideoProperties();
            }
        }

        #endregion

        #region IPlugin Properties

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        #endregion

        #region IS3DPlugin Properties

        /// <summary>
        /// Gets the 3D formats supported by this plugin.
        /// </summary>
        public S3D_Formats SupportedS3DFormats
        {
            get { return SupportedS3DFormatsInternal; }
        }

        /// <summary>
        /// Indicates whether the 3D Plugin is enabled.
        /// Allows a consuming application to instruct the 3D plugin to activate or deactivate.
        /// Useful when a consuming application will handle multuple 3D plugins.
        /// 
        /// Note: to properly enable/disable the Anaglyph 3D plugin, make sure to
        /// set this value before the Media Plugin loaded event fires.
        /// (the MediaPluginRegistered event fires before MediaPluginLoaded)
        /// </summary>
        public bool Is3DPluginEnabled
        {
            get { return pluginIsEnabled; }
            set
            {
                pluginIsEnabled = value;

                if (pluginIsEnabled)
                    InitializeAnaglyphSetup();
                else
                    UnloadAnaglyphSetup();
            }
        }

        /// <summary>
        /// Allows the MediaPresenterElement to be passed to the 3D plugin
        /// </summary>
        public ContentControl MediaPresenterElement
        {
            set { mediaPresenterElement = value; }
        }

        /// <summary>
        /// An event indicating that the S3D Properties have been successfully
        /// validated for this 3D plugin for the current Playlist Item
        /// </summary>
        public event EventHandler<S3DPropertiesEventArgs<S3DProperties>> S3DPropertiesValid;

        /// <summary>
        /// An event indicating that the S3D Properties are invalid for this 3D plugin
        /// for the current Playlist Item.
        /// </summary>
        public event EventHandler<S3DPropertiesEventArgs<S3DProperties>> S3DPropertiesInvalid;

        #endregion

        #region IPlugin Events

        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when an exception occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when an exception occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// 
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        #endregion

        #region IPlugin Methods

        /// <summary>
        /// Loads the plugin
        /// </summary>
        public void Load()
        {
            current = this;

            try
            {
                IsLoaded = true;
                PluginLoaded.IfNotNull(i => i(this));
            }
            catch (Exception err)
            {
                IsLoaded = false;
                PluginLoadFailed.IfNotNull(i => i(this, err));
            }
        }

        /// <summary>
        /// Unloads the plugin
        /// </summary>
        public void Unload()
        {
            try
            {
                IsLoaded = false;
                PluginUnloaded.IfNotNull(i => i(this));
            }
            catch (Exception err)
            {
                PluginUnloadFailed.IfNotNull(i => i(this, err));
            }
        }

        #endregion

        #region IGenericPlugin Methods

        /// <summary>
        /// Provides a mechanism to pass the SMF player to the plugin.
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(FrameworkElement player)
        {
            smfPlayer = player as SMFPlayer;
            smfPlayer.PlaylistItemChanged += smfPlayer_PlaylistItemChanged;
#if !OOB
            smfPlayer.FullScreenChanged += smfPlayer_FullScreenChanged;
#endif
            smfPlayer.SizeChanged += smfPlayer_SizeChanged;
            smfPlayer.PlaybackBitrateChanged += smfPlayer_PlaybackBitrateChanged;
            smfPlayer.PlayStateChanged += smfPlayer_PlayStateChanged;
            smfPlayer.MediaPluginRegistered += smfPlayer_MediaPluginRegistered;

            InitializeAnaglyphSetup();
        }

        #endregion

        #region Media Plugin Event Handlers

        void activeMediaPlugin_PluginLoaded(IPlugin obj)
        {
            //When the active Media Plugin is loaded, we'll determine whether the video content
            //is formatted for Anaglyph 3D.  If so, we'll take over the loading of the MediaPresenterElement's
            //content
            InitializeAnaglyphSetup();
            SetMediaPresenterElementContent();
        }

        #endregion

        #region SMF Player Event Handlers

        void smfPlayer_MediaPluginRegistered(object sender, CustomEventArgs<IMediaPlugin> e)
        {
            //When the media plugin (progressive or smooth streaming) is registered, we'll subscribe
            //to its loaded event so that we can begin the initialization of the Anaglyph setup.
            activeMediaPlugin = e.Value;

            //(Unsubscribing from any previously loaded plugins first):
            activeMediaPlugin.PluginLoaded -= new Action<IPlugin>(activeMediaPlugin_PluginLoaded);
            activeMediaPlugin.PluginLoaded += new Action<IPlugin>(activeMediaPlugin_PluginLoaded);
        }

        void smfPlayer_PlayStateChanged(object sender, CustomEventArgs<MediaPluginState> e)
        {
            SetVideoProperties();
        }

        void smfPlayer_PlaybackBitrateChanged(object sender, CustomEventArgs<long> e)
        {
            SetVideoProperties();
        }

        void smfPlayer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetVideoProperties();
        }

#if !OOB
        void smfPlayer_FullScreenChanged(object sender, EventArgs e)
        {
            SetVideoProperties();
        }
#endif

        void smfPlayer_PlaylistItemChanged(object sender, CustomEventArgs<Core.Media.PlaylistItem> e)
        {
            //A new playlist item has loaded - let's re-load the Anaglyph setup, or unload it if 
            //no longer applicable
            InitializeAnaglyphSetup();

            //Let's now raise events indicating whether this playlist item is valid for this 3D plugin.
            if (validPropertiesForAnaglyph)
            {
                //Raise an event indicating that this playlist item is valid for this plugin
                S3DPropertiesValid.IfNotNull(i => i(this, new S3DPropertiesEventArgs<S3DProperties>(smfPlayer.CurrentPlaylistItem.S3DProperties)));
            }
            else
            {
                //Raise an event indicating that this playlist item is invalid for this plugin
                if (smfPlayer.CurrentPlaylistItem != null &&
                    smfPlayer.CurrentPlaylistItem.S3DProperties != null)
                    S3DPropertiesInvalid.IfNotNull(i => i(this, new S3DPropertiesEventArgs<S3DProperties>(smfPlayer.CurrentPlaylistItem.S3DProperties)));
                else
                    S3DPropertiesInvalid.IfNotNull(i => i(this, null));
            }
        }

        #endregion

        #region Adaptive Media Plugin Event Handlers

        void AdaptiveMediaPlugin_VideoPlaybackTrackChanged(IAdaptiveMediaPlugin arg1, IMediaTrack videoPlaybackTrack)
        {
            //When the SSME changes its playback bitrate, we need to re-set the maximum width and height of the
            //SSME explicitly.
            if (videoPlaybackTrack.Attributes.ContainsKey("MaxWidth"))
                ssmeMaxWidth = Convert.ToDouble(videoPlaybackTrack.Attributes["MaxWidth"]);
            if (videoPlaybackTrack.Attributes.ContainsKey("MaxHeight"))
                ssmeMaxHeight = Convert.ToDouble(videoPlaybackTrack.Attributes["MaxHeight"]);
            SetVideoProperties();
        }

        #endregion

        #region Anaglyph3DPlugin Loading/Unloading Methods

        void InitializeAnaglyphSetup()
        {
            if (smfPlayer.CurrentPlaylistItem != null)
            {
                //Only ever initialize the Anaglyph setup if the current playlist item specifies
                //a format that works for Anaglyph
                ValidateS3DPropertiesforAnaglyph();

                if (pluginIsEnabled && validPropertiesForAnaglyph)
                {
                    //Anaglyph is supported by the S3D properties specified on the current
                    //playlist item - let's load the Anaglyph setup
                    LoadAnaglyphSetup();
                }
                else
                {
                    //Anaglyph is not supported by the S3D properties specified on the current
                    //playlist item - let's unload the Anaglyph setup
                    UnloadAnaglyphSetup();
                }
            }
        }

        /// <summary>
        /// Ensures that the content conforms to S3D properties that can be properly
        /// rendered in Anaglyph
        /// </summary>
        void ValidateS3DPropertiesforAnaglyph()
        {
            //Note: the Left Eye's Pixel Aspect ratio is the PAR that will be used here
            //We assume that the left and right eye PAR match, as the Anaglyph implementation
            //doesn't handle for mismatched PARs.
            if (smfPlayer.CurrentPlaylistItem != null &&
                smfPlayer.CurrentPlaylistItem.S3DProperties != null &&
                ((smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.SideBySide) ||
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.TopAndBottom)) &&
                ((smfPlayer.CurrentPlaylistItem.S3DProperties.S3DContent == S3D_Contents.None) ||
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DContent == S3D_Contents.Pair)) &&
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DSubsamplingModes == S3D_SubsamplingModes.None) &&
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DSubsamplingOrders == S3D_SubsamplingOrders.None) &&
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR > 0.0) &&
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DRightEyePAR > 0.0) &&
                (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR ==
                    smfPlayer.CurrentPlaylistItem.S3DProperties.S3DRightEyePAR)
                )
                validPropertiesForAnaglyph = true;
            else
                validPropertiesForAnaglyph = false;

        }

        /// <summary>
        /// Grabs the active Media Element or Smooth Streaming Media element from the 
        /// active media plugin, and begins the 3D rendering process by kicking off
        /// the SetEffect() method.
        /// </summary>
        void LoadAnaglyphSetup()
        {
            if ((activeMediaPlugin != null) && (activeMediaPlugin.VisualElement != null) && pluginIsEnabled)
            {
                if (activeMediaPlugin is IAdaptiveMediaPlugin)
                {
                    isSmoothStream = true;

                    //Subscribe to the Adaptive Media Plugin's playback track changed event so that we can
                    //grab the MaxHeight and MaxWidth of the current track/bitrate.  We need this
                    //since the SSME reports its Natural Width/Height as the top bitrate regardless of
                    //the currently playing bitrate.
                    ((IAdaptiveMediaPlugin)activeMediaPlugin).VideoPlaybackTrackChanged -= AdaptiveMediaPlugin_VideoPlaybackTrackChanged;
                    ((IAdaptiveMediaPlugin)activeMediaPlugin).VideoPlaybackTrackChanged += AdaptiveMediaPlugin_VideoPlaybackTrackChanged;
                }
                else
                    isSmoothStream = false;

                SetEffect();
            }
        }

        /// <summary>
        /// Start the process of undoing any modifications made to visual elements so that new
        /// non-3D content can load normally.
        /// </summary>
        void UnloadAnaglyphSetup()
        {
            //Rename the Canvas this plugin inserted into the Content of the 
            //mediaPresenterElement so that the core SMF knows it can re-load 
            //new content into the mediaPresenterElement.
            if ((mediaPresenterElement != null) && (mediaPresenterElement.Content != null))
            {
                if (mediaPresenterElement.Content.GetType() == typeof(Canvas))
                {
                    Canvas canvas = mediaPresenterElement.Content as Canvas;
                    if (canvas.Name == "PluginInjectedCanvas")
                        canvas.Name = "PluginUnloadedCanvas";
                }
            }

            //Remove pixel shader effects
            RemoveEffect();
        }

        /// <summary>
        /// Allows the specific Anaglyph display mechanism to be passed in to the plugin
        /// by a consuming application.
        /// </summary>
        /// <param name="displayMechanism"></param>
        public void SetActiveDisplayMechanism(AnaglyphDisplayMechanism displayMechanism)
        {
            ActiveDisplayMechanism = displayMechanism;
        }

        #endregion

        #region Visual Element Property Modification Methods

        /// <summary>
        /// In the SMF visual tree, the MediaPresenterElement contains the MediaElement
        /// or SmoothStreamingMediaElement.  In order to properly render anaglyph 3D content,
        /// a Canvas must be inserted between the MediaPresenter Element and the ME/SSME.
        /// 
        /// This method takes over the loading of content into the MediaPresenterElement, injecting
        /// a Canvas to allow for 3D rendering.  
        /// 
        /// We'll name the Canvas we insert "PluginInjectedCanvas".  This will allow the Core SMF
        /// code to detect that the MediaPresenterElement's content has been custom-loaded by a 
        /// plugin, which will cause it to skip settin the MediaPresenterElement's content.
        /// </summary>
        private void SetMediaPresenterElementContent()
        {
            if ((mediaPresenterElement != null) && (validPropertiesForAnaglyph) && pluginIsEnabled)
            {
                canvas = new Canvas();
                canvas.Name = "PluginInjectedCanvas";
                mediaPresenterElement.Content = canvas;
                canvas.Children.Add(activeMediaPlugin.VisualElement);

                mediaPresenterElement.UpdateLayout();
            }
        }

        /// <summary>
        /// Chooses to route to a sub-method for either ME or SSME to 
        /// set the ME/SSME's shader effect and choose a scaling factor.
        /// The method that modifies the visual elements' properties will then
        /// be kicked off.  
        /// </summary>
        private void SetEffect()
        {
            SetShaderEffect();
            SetVideoProperties();
        }

        /// <summary>
        /// Removes the shader effect from the ME/SSME so that non-3D content
        /// can be rendered.
        /// </summary>
        private void RemoveEffect()
        {
            if (activeMediaPlugin != null && activeMediaPlugin.VisualElement != null)
                activeMediaPlugin.VisualElement.Effect = null;

            RemoveVideoProperties();
        }

        /// <summary>
        /// Chooses to route to a sub-method for either ME or SSME to 
        /// set the properties on the visual elements to support proper
        /// anaglyph 3D rendering.
        /// </summary>
        private void SetVideoProperties()
        {
            SetVisualElementVideoProperties();
        }

        /// <summary>
        /// Removes the property modifications made to the visual elements
        /// for 3D rendering so that non-3D content can be displayed as normal.
        /// Since a new SSME or ME should be loaded with the next playlist item,
        /// this method is more of a safety catch.
        /// </summary>
        private void RemoveVideoProperties()
        {
            if (activeMediaPlugin != null && activeMediaPlugin.VisualElement != null)
            {
                activeMediaPlugin.Stretch = smfPlayer.CurrentPlaylistItem.VideoStretchMode;
                activeMediaPlugin.VisualElement.Clip = null;
                activeMediaPlugin.VisualElement.SetValue(Canvas.TopProperty, 0.0);
                activeMediaPlugin.VisualElement.SetValue(Canvas.LeftProperty, 0.0);
            }

            mediaPresenterElement.CacheMode = null;
            mediaPresenterElement.RenderTransform = null;
        }

        #endregion

        #region Anaglyph Shader/Transform Methods

        /// <summary>
        /// Set the ME's or SSME's shader effect and chooses a scaling factor.
        /// </summary>
        private void SetShaderEffect()
        {
            if (activeMediaPlugin != null &&
                activeMediaPlugin.VisualElement != null &&
                validPropertiesForAnaglyph &&
                pluginIsEnabled)
            {
                switch (ActiveDisplayMechanism)
                {
                    case AnaglyphDisplayMechanism.None:
                        activeMediaPlugin.VisualElement.Effect = null;
                        XFactor = 1.0;
                        YFactor = 1.0;
                        break;
                    case AnaglyphDisplayMechanism.AnaglyphLeftOnly:
                        if (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.TopAndBottom)
                        {
                            activeMediaPlugin.VisualElement.Effect = null;
                            //Scale vertically by the Pixel Aspect Ratio (PAR)
                            XFactor = 1.0;
                            YFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                        }
                        else
                        {
                            activeMediaPlugin.VisualElement.Effect = null;
                            //Scale horizontally by the Pixel Aspect Ratio (PAR)
                            XFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                            YFactor = 1.0;
                        }
                        break;
                    case AnaglyphDisplayMechanism.AnaglyphGrayRedCyan:
                        GrayAnaglyphRedCyan grayAnaglyphRedCyan = new GrayAnaglyphRedCyan();
                        //Passing in the eye priority as a double.  It's formatted as a double rather
                        //than a bool as that's the parameter format the pixel shader supports
                        grayAnaglyphRedCyan.LeftEyeHasPriority =
                            (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DEyePriority == S3D_EyePriorities.LeftFirst)
                            ? 1.0 : 0.0;

                        if (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.TopAndBottom)
                        {
                            //Since we're using the "Top and Bottom" format, we'll set the
                            //"SideBySide" property to "0.0" (false).  It's formatted as a double rather
                            //than a bool as that's the parameter format the pixel shader supports
                            grayAnaglyphRedCyan.SideBySide = 0.0;

                            activeMediaPlugin.VisualElement.Effect = grayAnaglyphRedCyan;
                            //Scale vertically by the Pixel Aspect Ratio (PAR)
                            XFactor = 1.0;
                            YFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                        }
                        else
                        {
                            //Since we're using the "SideBySide" format, we'll set the
                            //"SideBySide" property to "1.0" (true).  It's formatted as a double rather
                            //than a bool as that's the parameter format the pixel shader supports
                            grayAnaglyphRedCyan.SideBySide = 1.0;

                            activeMediaPlugin.VisualElement.Effect = grayAnaglyphRedCyan;
                            //Scale horizontally by the Pixel Aspect Ratio (PAR)
                            XFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                            YFactor = 1.0;
                        }
                        break;
                    case AnaglyphDisplayMechanism.AnaglyphHalfColorRedCyan:
                        HalfColorRedCyanAnaglyph halfColorRedCyanAnaglyph = new HalfColorRedCyanAnaglyph();
                        //Passing in the eye priority as a double.  It's formatted as a double rather
                        //than a bool as that's the parameter format the pixel shader supports
                        halfColorRedCyanAnaglyph.LeftEyeHasPriority =
                            (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DEyePriority == S3D_EyePriorities.LeftFirst)
                            ? 1.0 : 0.0;

                        if (smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.TopAndBottom)
                        {
                            //Since we're using the "Top and Bottom" format, we'll set the
                            //"SideBySide" property to "0.0" (false).  It's formatted as a double rather
                            //than a bool as that's the parameter format the pixel shader supports
                            halfColorRedCyanAnaglyph.SideBySide = 0.0;

                            activeMediaPlugin.VisualElement.Effect = halfColorRedCyanAnaglyph;
                            //Scale vertically by the Pixel Aspect Ratio (PAR)
                            XFactor = 1.0;
                            YFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                        }
                        else
                        {
                            //Since we're using the "SideBySide" format, we'll set the
                            //"SideBySide" property to "1.0" (true).  It's formatted as a double rather
                            //than a bool as that's the parameter format the pixel shader supports
                            halfColorRedCyanAnaglyph.SideBySide = 1.0;

                            activeMediaPlugin.VisualElement.Effect = halfColorRedCyanAnaglyph;
                            //Scale horizontally by the Pixel Aspect Ratio (PAR)
                            XFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DLeftEyePAR;
                            YFactor = 1.0;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the properties on the visual elements to support proper
        /// anaglyph 3D rendering.
        /// </summary>
        private void SetVisualElementVideoProperties()
        {
            if (activeMediaPlugin != null &&
                activeMediaPlugin.VisualElement != null &&
                mediaPresenterElement != null &&
                validPropertiesForAnaglyph &&
                pluginIsEnabled)
            {
                if (canvas != null &&
                    activeMediaPlugin.NaturalVideoSize.Width > 0 &&
                    activeMediaPlugin.NaturalVideoSize.Height > 0)
                {
                    activeMediaPlugin.VisualElement.Clip = new RectangleGeometry();
                    canvas.RenderTransform = new ScaleTransform();

                    //Turn ON bitmap caching (GPU acceleration) on the Canvas to let the GPU stretch
                    //the image, but CacheMode/GPU Acceleration OFF on the MediaElement so that 
                    //the pixel shaders are applied BEFORE the image is scaled up/stretched
                    canvas.CacheMode = new BitmapCache();

                    activeMediaPlugin.VisualElement.CacheMode = null;
                    activeMediaPlugin.EnableGPUAcceleration = false;

                    //If a progressive stream,
                    //Set Stretch="None" to ensure the MediaElement decodes the video at native resolution
                    if (!isSmoothStream)
                        activeMediaPlugin.Stretch = Stretch.None;

                    //If a SSME,
                    //Note that The underlying MediaElement under the SSME ignores the width and 
                    //height set on the SSME if the Stretch is set to None, so we'll leave it
                    //at the default of "Uniform".  The idea here is to make sure that the 
                    //MediaElement is decoding the video at the native resolution, which 
                    //will still happen with Stretch="Uniform".

                    //Set the width and height on the SSME so that the pixel shaders are applied
                    //to the native resolution of the currently playing bitrate
                    if (isSmoothStream)
                    {
                        if (ssmeMaxWidth > 0 && ssmeMaxHeight > 0)
                        {
                            activeMediaPlugin.VisualElement.Width = ssmeMaxWidth;
                            activeMediaPlugin.VisualElement.Height = ssmeMaxHeight;
                        }
                        else if (activeMediaPlugin.NaturalVideoSize.Width > 0 && activeMediaPlugin.NaturalVideoSize.Height > 0)
                        {
                            activeMediaPlugin.VisualElement.Width = activeMediaPlugin.NaturalVideoSize.Width;
                            activeMediaPlugin.VisualElement.Height = activeMediaPlugin.NaturalVideoSize.Height;
                        }
                    }

                    //Otherwise, for a MediaElement,
                    //set the width and height on the ME so that the pixel shaders are applied
                    //to the native resolution 
                    else if (activeMediaPlugin.NaturalVideoSize.Width > 0 && activeMediaPlugin.NaturalVideoSize.Height > 0)
                    {
                        activeMediaPlugin.VisualElement.Width = activeMediaPlugin.NaturalVideoSize.Width;
                        activeMediaPlugin.VisualElement.Height = activeMediaPlugin.NaturalVideoSize.Height;
                    }

                    double clipXFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.SideBySide ? 2 : 1;
                    double clipYFactor = smfPlayer.CurrentPlaylistItem.S3DProperties.S3DFormat == S3D_Formats.TopAndBottom ? 2 : 1;

                    double trueWidth = (activeMediaPlugin.VisualElement.Width / clipXFactor);
                    double trueHeight = (activeMediaPlugin.VisualElement.Height / clipYFactor);

                    double displayWidth = XFactor * trueWidth;
                    double displayHeight = YFactor * trueHeight;

                    //Depending on the stretch mode, set the stretch to fill or Uniform via
                    //manually setting the scale factor
                    if (StretchMode == StretchModes.Fill)
                    {
                        double scaleX = canvas.ActualWidth / trueWidth;
                        double scaleY = canvas.ActualHeight / trueHeight;
                        //Ignores aspect ratio and fills to the canvas
                        ((ScaleTransform)canvas.RenderTransform).ScaleX = scaleX;
                        ((ScaleTransform)canvas.RenderTransform).ScaleY = scaleY;
                    }
                    else if (StretchMode == StretchModes.Uniform)
                    {
                        //Maintains aspect ratio
                        AdjustAspectRatioToUniform(displayWidth, displayHeight);
                    }

                    //Finally, clip the video to half its size
                    ((RectangleGeometry)activeMediaPlugin.VisualElement.Clip).Rect = new Rect(0, 0, trueWidth, trueHeight);
                }
            }
        }

        /// <summary>
        /// Maintains the aspect ratio for scaled content in the ME or SSME
        /// </summary>
        private void AdjustAspectRatioToUniform(double maxWidth, double maxHeight)
        {
            if (maxWidth > 0 && maxHeight > 0)
            {
                double videoAspectRatio = maxWidth / maxHeight;
                double canvasAspectRatio = (double)canvas.ActualWidth / (double)canvas.ActualHeight;

                if (canvasAspectRatio > videoAspectRatio)
                {
                    //Adjust the X scaling factor down to match the aspect ratio defined by the height
                    double scalingFactor = canvas.ActualHeight / maxHeight;
                    ((ScaleTransform)canvas.RenderTransform).ScaleY = YFactor * scalingFactor;
                    ((ScaleTransform)canvas.RenderTransform).ScaleX = XFactor * scalingFactor;

                    //Now that we've set up the scaling, we need to center the video horizontally
                    activeMediaPlugin.VisualElement.SetValue(Canvas.TopProperty, 0.0);
                    double adjustedVideoWidth = scalingFactor * maxWidth;
                    double videoLeftStartPoint = (canvas.ActualWidth - adjustedVideoWidth) / 2;
                    if (((ScaleTransform)canvas.RenderTransform).ScaleX > 0)
                    {
                        double scaledVideoLeftStartPoint = videoLeftStartPoint / ((ScaleTransform)canvas.RenderTransform).ScaleX;
                        activeMediaPlugin.VisualElement.SetValue(Canvas.LeftProperty, scaledVideoLeftStartPoint);
                    }
                }
                else
                {
                    //Adjust the Y scaling factor down to match the aspect ratio defined by the width
                    double scalingFactor = canvas.ActualWidth / maxWidth;
                    ((ScaleTransform)canvas.RenderTransform).ScaleX = XFactor * scalingFactor;
                    ((ScaleTransform)canvas.RenderTransform).ScaleY = YFactor * scalingFactor;

                    //Now that we've set up the scaling, we need to center the video vertically
                    activeMediaPlugin.VisualElement.SetValue(Canvas.LeftProperty, 0.0);
                    double adjustedVideoHeight = scalingFactor * maxHeight;
                    double videoTopStartPoint = (canvas.ActualHeight - adjustedVideoHeight) / 2;
                    if (((ScaleTransform)canvas.RenderTransform).ScaleY > 0)
                    {
                        double scaledVideoTopStartPoint = videoTopStartPoint / ((ScaleTransform)canvas.RenderTransform).ScaleY;
                        activeMediaPlugin.VisualElement.SetValue(Canvas.TopProperty, scaledVideoTopStartPoint);
                    }
                }
            }
        }

        #endregion

    }
}
