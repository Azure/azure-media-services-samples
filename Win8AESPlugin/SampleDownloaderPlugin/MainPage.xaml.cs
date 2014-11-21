using CloudVideoClient.Xaml.Crypto;
using Microsoft.Media.AdaptiveStreaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleDownloaderPlugin
{

    public class SampleCostants
    {
        public const string URL = "PUT_IN_STREAMING_URL";

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaExtensionManager extensions = new MediaExtensionManager();
        private Windows.Foundation.Collections.PropertySet propertySet = new Windows.Foundation.Collections.PropertySet();
        private IAdaptiveSourceManager adaptiveSourceManager;
        private AdaptiveSource adaptiveSource = null;
        private AdaptiveSourceStatusUpdatedEventArgs adaptiveSourceStatusUpdate;

        public MainPage()
        {
            this.InitializeComponent();

            // Gets the default instance of AdaptiveSourceManager which manages Smooth 
            //Streaming media sources.
            adaptiveSourceManager = AdaptiveSourceManager.GetDefault();
            // Sets property key value to AdaptiveSourceManager default instance.
            // {A5CE1DE8-1D00-427B-ACEF-FB9A3C93DE2D}" must be hardcoded.
            propertySet["{A5CE1DE8-1D00-427B-ACEF-FB9A3C93DE2D}"] = adaptiveSourceManager;

            // Registers Smooth Streaming byte-stream handler for “.ism” extension and, 
            // "text/xml" and "application/vnd.ms-ss" mime-types and pass the propertyset. 
            // http://*.ism/manifest URI resources will be resolved by Byte-stream handler.
            extensions.RegisterByteStreamHandler("Microsoft.Media.AdaptiveStreaming.SmoothByteStreamHandler", ".ism", "text/xml", propertySet);
            extensions.RegisterByteStreamHandler("Microsoft.Media.AdaptiveStreaming.SmoothByteStreamHandler", ".ism", "application/vnd.ms-ss", propertySet);
            extensions.RegisterSchemeHandler("Microsoft.Media.AdaptiveStreaming.SmoothSchemeHandler", "ms-sstr:", propertySet);

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Comment the line below to disable downloader plugin
           // adaptiveSourceManager.SetDownloaderPlugin(new AdaptiveStreamDecoder.DecoderDownloadPlugin());


            adaptiveSourceManager.SetDownloaderPlugin(new DecoderDownloadPlugin());

            myPlayer.AutoPlay = true;
            myPlayer.Visibility = Windows.UI.Xaml.Visibility.Visible;
            myPlayer.Source = new Uri(SampleCostants.URL, UriKind.Absolute);

        }
    }


    
    }



