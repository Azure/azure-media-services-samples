using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Resources;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Provides SMF with the ability to parse and display DFXP formatted Timed Text captions arriving over in-stream data tracks.
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMarkerProvider(PluginName = PluginName,
        PluginDescription = PluginDescription,
        PluginVersion = PluginVersion,
        SupportedFormat = SupportedFormat,
        SupportsPolling = false)]
    public class InstreamMastMarkerProvider : MastMarkerProviderBase
    {
        private const string PluginName = "InstreamMastMarkerProvider";
        private const string PluginDescription = "Provides SMF with the ability to schedule MAST ads arriving over in-stream data tracks.";
        private const string PluginVersion = "2.2012.0605.0";
        
        public const string SupportedFormat = "INSTREAM-MAST";

        private const string TypeAttribute = "type";
        private const string SubTypeAttribute = "subtype";

        private static readonly ReadOnlyCollection<string> AllowedStreamSubTypes = new ReadOnlyCollection<string>(new List<string> { "MAST" });

        public override void Load()
        {
            if (player.ActiveMediaPlugin is IAdaptiveMediaPlugin)
            {
                // the ManifestReady event will be the first opportunity to select the MAST stream
                var activeMediaPlugin = player.ActiveMediaPlugin as IAdaptiveMediaPlugin;
                activeMediaPlugin.ManifestReady += activeMediaPlugin_ManifestReady;
            }
            base.Load();
        }

        void activeMediaPlugin_ManifestReady(IAdaptiveMediaPlugin activeMediaPlugin)
        {
            activeMediaPlugin.ManifestReady -= activeMediaPlugin_ManifestReady;

            // find all MAST streams
            selectedMastStreams = activeMediaPlugin.CurrentSegment.AvailableStreams.Where(i => IsMastStream(i)).ToList();

            if (selectedMastStreams.Any())
            {
                // wire up the DataReceived event
                player.DataReceived += Player_DataReceived;

                // select all MAST streams
                activeMediaPlugin.ModifySegmentSelectedStreams(activeMediaPlugin.CurrentSegment, selectedMastStreams, new List<IMediaStream>());
            }
            else 
            {
                // no MAST streams were found, release the player so playback can continue
                base.ReleasePlayer();
            }
        }

        public override void Unload()
        {
            player.DataReceived -= Player_DataReceived;
            base.Unload();
        }

        private List<IMediaStream> selectedMastStreams;

        public override void StopRetrievingMarkers()
        {
            if (player.ActiveMediaPlugin is IAdaptiveMediaPlugin)
            {
                var ActiveAdaptiveMediaPlugin = player.ActiveMediaPlugin as IAdaptiveMediaPlugin;
                if (selectedMastStreams != null && selectedMastStreams.Any())
                {
                    ActiveAdaptiveMediaPlugin.ModifySegmentSelectedStreams(ActiveAdaptiveMediaPlugin.CurrentSegment, new List<IMediaStream>(), selectedMastStreams);
                }
            }
            selectedMastStreams = null;
            base.StopRetrievingMarkers();
        }

        private void Player_DataReceived(object sender, DataReceivedInfo args)
        {
            if (IsMastStream(args.StreamAttributes))
            {
                try
                {
                    var dataAsString = System.Text.Encoding.UTF8.GetString(args.Data, 0, args.Data.Length);
                    base.LoadMastDoc(dataAsString);
                }
                catch (Exception error)
                {
                    ReleasePlayer();

                    string logMessage = string.Format(MastMarkerProviderResources.DownloadFailedLogMessage, error.Message);
                    SendLogEntry(LogEntryTypes.DownloadFailed, LogLevel.Warning, logMessage);
                    OnRetrieveMarkersFailed(error);
                }
            }
        }
        
        private static bool IsMastStream(IDictionary<string, string> streamAttributes)
        {
            StreamType result;
            string type = streamAttributes.GetEntryIgnoreCase(TypeAttribute);
            string subType = streamAttributes.GetEntryIgnoreCase(SubTypeAttribute);

            return type.EnumTryParse(true, out result) && result == StreamType.Text
                   && AllowedStreamSubTypes.Any(i => string.Equals(i, subType, StringComparison.CurrentCultureIgnoreCase));
        }


        private static bool IsMastStream(IMediaStream mediaStream)
        {
            return mediaStream.Type == StreamType.Text
                   && AllowedStreamSubTypes.Any(i => string.Equals(i, mediaStream.SubType, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
