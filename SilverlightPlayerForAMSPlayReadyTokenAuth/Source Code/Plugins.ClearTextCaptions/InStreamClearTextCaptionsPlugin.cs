using System;
using System.Linq;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using System.ComponentModel.Composition;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using System.Diagnostics;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.ClearTextCaptions
{
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InStreamClearTextCaptionsPlugin : IGenericPlugin
    {
        private const string PluginName = "InStreamClearTextCaptionsPlugin";
        private const string TypeAttribute = "type";
        private const string SubTypeAttribute = "subtype";
        private const string PluginDescription =
            "Provides SMF with the ability to parse and display clear-text formatted Timed Text captions arriving over in-stream data tracks.";
        private const string PluginVersion = "0.1.0.0";

        public event Action<IPlugin, LogEntry> LogReady;
        public event Action<IPlugin, Exception> PluginLoadFailed;
        public event Action<IPlugin> PluginLoaded;
        public event Action<IPlugin, Exception> PluginUnloadFailed;
        public event Action<IPlugin> PluginUnloaded;

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamSubTypes =
            new ReadOnlyCollection<string>(new List<string> { "CAPT", "SUBT", "SMPTE-TT-608" });

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamFourCCValues =
            new ReadOnlyCollection<string>(new List<string> { "TTML", "DFXP" });

        private SMFPlayer _player;
        private readonly CaptionRegion _captionRegion;

        public bool IsLoaded { get; private set; }

        public InStreamClearTextCaptionsPlugin()
        {
            _captionRegion = new CaptionRegion();
        }

        public void SetPlayer(FrameworkElement player)
        {
            _player = player as SMFPlayer;
        }

        public void Load()
        {
            if (_player != null)
            {
                _player.DataReceived += Player_DataReceived;
                _player.MediaOpened += Player_MediaOpened;
            }

            IsLoaded = true;
            PluginLoaded.IfNotNull(i => i(this));
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            _player.Captions.Add(_captionRegion);
        }

        private void Player_DataReceived(object sender, DataReceivedInfo e)
        {
            if (IsCaptionStream(e.StreamAttributes))
            {
                try
                {
                    var stream = new MemoryStream(e.Data);
                    var text = new StreamReader(stream).ReadToEnd();
                    var duration = e.DataChunk.Duration != TimeSpan.Zero
                                        ? e.DataChunk.Duration
                                        : TimeSpan.FromMilliseconds(2002);

                    if (_captionRegion.Begin == TimeSpan.MinValue || _captionRegion.Begin > e.DataChunk.Timestamp)
                    {
                        _captionRegion.Begin = e.DataChunk.Timestamp;
                    }

#if SILVERLIGHT3
                    if (!SystemExtensions.IsNullOrWhiteSpace(text))
#else
                    if (!string.IsNullOrWhiteSpace(text))
#endif
                    {
                        var caption = new CaptionElement
                        {
                            Content = text,
                            Begin = e.DataChunk.Timestamp,
                            End = e.DataChunk.Timestamp.Add(duration)
                        };

                        _captionRegion.Children.Add(caption);
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine(err.Message);
                }
            }
        }


        public void Unload()
        {
            if (_player != null)
            {
                _player.DataReceived -= Player_DataReceived;
                _player.MediaOpened -= Player_MediaOpened;
                _player.Captions.Remove(_captionRegion);
            }

            IsLoaded = false;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        private static bool IsCaptionStream(IDictionary<string, string> streamAttributes)
        {
            StreamType result;
            string type = streamAttributes.GetEntryIgnoreCase(TypeAttribute);
            string subType = streamAttributes.GetEntryIgnoreCase(SubTypeAttribute);

#if SILVERLIGHT3
            return SystemExtensions.TryParse(type, true, out result)
#else
            return Enum.TryParse(type, true, out result)
#endif
                && result == StreamType.Text
                && AllowedCaptionStreamSubTypes.Any(i => string.Equals(i, subType, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
