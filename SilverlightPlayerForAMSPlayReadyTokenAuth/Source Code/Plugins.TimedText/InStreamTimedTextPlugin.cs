using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Microsoft.SilverlightMediaFramework.Core;
using Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions;
using Microsoft.SilverlightMediaFramework.Plugins.Metadata;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Plugins.TimedText.Resources;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;
using System.Diagnostics;
using Microsoft.SilverlightMediaFramework.Core.Media;
using System.ComponentModel;
using System.Threading;
#if USEAFFINITY
using Microsoft.Xbox.Core;
#endif

namespace Microsoft.SilverlightMediaFramework.Plugins.TimedText
{
    /// <summary>
    /// Provides SMF with the ability to parse and display DFXP formatted Timed Text captions arriving over in-stream data tracks.
    /// </summary>
    [ExportGenericPlugin(PluginName = PluginName, PluginDescription = PluginDescription, PluginVersion = PluginVersion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InStreamTimedTextPlugin : IGenericPlugin
    {
        private const string PluginName = "InStreamTimedTextPlugin";
        private const string TypeAttribute = "type";
        private const string SubTypeAttribute = "subtype";


        private const string PluginDescription =
            "Provides SMF with the ability to parse and display DFXP formatted Timed Text captions arriving over in-stream data tracks.";

        private const string PluginVersion = "2.2012.0605.0";

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamSubTypes =
            new ReadOnlyCollection<string>(new List<string> { "CAPT", "SUBT", "SMPTE-TT-608" });

        private static readonly ReadOnlyCollection<string> AllowedCaptionStreamFourCCValues =
            new ReadOnlyCollection<string>(new List<string> { "TTML", "DFXP" });

        private IPlayer _player;

        #region IGenericPlugin Members
        /// <summary>
        /// Occurs when the log is ready.
        /// </summary>
        public event Action<IPlugin, LogEntry> LogReady;

        /// <summary>
        /// Occurs when the plug-in is loaded.
        /// </summary>
        public event Action<IPlugin> PluginLoaded;

        /// <summary>
        /// Occurs when the plug-in is unloaded.
        /// </summary>
        public event Action<IPlugin> PluginUnloaded;

        /// <summary>
        /// Occurs when the plug-in fails to load.
        /// </summary>
        public event Action<IPlugin, Exception> PluginLoadFailed;

        /// <summary>
        /// Occurs when the plug-in fails to unload.
        /// </summary>
        public event Action<IPlugin, Exception> PluginUnloadFailed;

        /// <summary>
        /// Gets a value indicating whether a plug-in is currently loaded.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Passes the plugin a reference to the Player
        /// </summary>
        /// <param name="player">A reference to the Player</param>
        public void SetPlayer(FrameworkElement player)
        {
            _player = player as IPlayer;
        }

        /// <summary>
        /// Loads the plug-in.
        /// </summary>
        public void Load()
        {
            IsLoaded = true;
            _player.IfNotNull(i => i.DataReceived += Player_DataReceived);
            PluginLoaded.IfNotNull(i => i(this));
        }

        /// <summary>
        /// Unloads the plug-in.
        /// </summary>
        public void Unload()
        {
            IsLoaded = false;
            _player.IfNotNull(i => i.DataReceived -= Player_DataReceived);
            _player = null;
            PluginUnloaded.IfNotNull(i => i(this));
        }

        #endregion

        Queue<WorkLoad> workerQueue = new Queue<WorkLoad>();

        class WorkLoad
        {
            public TimeSpan timeOffset;
            public TimeSpan endTime;
            public byte[] data;
        }

        private void Player_DataReceived(object sender, DataReceivedInfo args)
        {
            if (IsCaptionStream(args.StreamAttributes))
            {
                var player = (SMFPlayer)sender;
                var workload = new WorkLoad()
                    {
                        data = args.Data,
                        timeOffset = player.CalculateRelativeMediaPosition(args.DataChunk.Timestamp),
                        //Added a workaround for an issue w/ the SSME that causes the
                        //duration data to be empty.  In this case we'll default to 2 seconds
                        //TODO: Remove this once the issue has been fix in the SSME.
                        endTime = args.DataChunk.Timestamp.Add(args.DataChunk.Duration != TimeSpan.Zero
                                            ? args.DataChunk.Duration
                                            : TimeSpan.FromMilliseconds(2002))
                    };

                workerQueue.Enqueue(workload);
                if (workerQueue.Count == 1)
                {
                    RunWorkLoad(workload);
                }
            }
        }

        void RunWorkLoad(WorkLoad workload)
        {
            Thread thread = new Thread(Operation);
            thread.Start(workload);
        }

        void Operation(object data)
        {
#if USEAFFINITY
            // The "extension" method SetThreadProcessorAffinity was missing the "this" notation, 
            // so it's not really an extension hence this style of call.
            RuntimeExtensions.SetThreadProcessorAffinity(Thread.CurrentThread, 4);  
#endif
            try
            {
                var workload = data as WorkLoad;
                XDocument document;
                using (var stream = new MemoryStream(workload.data))
                {
                    document = XDocument.Load(stream);
                }
                var body = document.Root.Element("{http://www.w3.org/2006/10/ttaf1}body");
                if (body != null && body.HasElements == false)
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OperationSucceeded(null));
                }
                else
                {
                    var parser = new TimedTextMarkerParser();
                    var markers = parser.ParseMarkerCollection(document, workload.timeOffset, workload.endTime);
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OperationSucceeded(markers));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => OperationFailed(ex));
            }
        }

        void OperationSucceeded(IEnumerable<CaptionRegion> markers)
        {
            try
            {
                if (_player != null)
                {
                    var workload = workerQueue.Peek();
                    LogEntry logEntry = null;
                    if (markers != null)
                    {
                        UpdateCaptions(((SMFPlayer)_player).Captions, markers);
                        logEntry = new LogEntry
                        {
                            Type = KnownLogEntryTypes.InStreamTimedTextPluginMarkersParsed,
                            SenderName = typeof(InStreamTimedTextPlugin).Name,
                            Severity = LogLevel.Information,
                            Message =
                                string.Format(
                                    TimedTextMediaPluginResources.
                                        InStreamTimedTextPluginParsingSuccessfulLogMessage, markers.Count())
                        };
                    }


                    if (logEntry != null)
                    {
                        LogReady.IfNotNull(i => i(this, logEntry));
                    }
                }
            }
            finally
            {
                workerQueue.Dequeue();
                if (workerQueue.Any())
                {
                    RunWorkLoad(workerQueue.Peek());
                }
            }
        }

        void OperationFailed(Exception err)
        {
            try
            {
                if (_player != null)
                {
                    var workload = workerQueue.Peek();
                    LogEntry logEntry = null;
                    {
                        logEntry = new LogEntry
                        {
                            Type = KnownLogEntryTypes.InStreamTimedTextPluginErrorOccurred,
                            SenderName = typeof(InStreamTimedTextPlugin).Name,
                            Severity = LogLevel.Error,
                            Message =
                                string.Format(
                                    TimedTextMediaPluginResources.
                                        InStreamTimedTextPluginParseDFXPFailedLogMessage, err.Message)
                        };
                    }

                    if (logEntry != null)
                    {
                        LogReady.IfNotNull(i => i(this, logEntry));
                    }
                }
            }
            finally
            {
                workerQueue.Dequeue();
                if (workerQueue.Any())
                {
                    RunWorkLoad(workerQueue.Peek());
                }
            }
        }

        private static void UpdateCaptions<TElement>(MediaMarkerCollection<TElement> originalElements, IEnumerable<TElement> newElements) where TElement : TimedTextElement
        {
            foreach (var updatedElement in newElements)
            {
                if (originalElements.ContainsId(updatedElement.Id))
                {
                    bool updated = false;
                    var originalElement = originalElements.GetById(updatedElement.Id);

                    if (originalElement.Begin > updatedElement.Begin)
                    {
                        updated = true;
                        originalElement.Begin = updatedElement.Begin;
                    }

                    if (originalElement.End < updatedElement.End)
                    {
                        updated = true;
                        originalElement.End = updatedElement.End;
                    }

                    if (updated)
                    {
                        originalElement.NotifyPositionChanged();
                    }

                    UpdateCaptions(originalElement.Children, updatedElement.Children);
                }
                else
                {
                    originalElements.Add(updatedElement);
                }
            }
        }

        private static bool IsCaptionStream(IDictionary<string, string> streamAttributes)
        {
            StreamType result;
            string type = streamAttributes.GetEntryIgnoreCase(TypeAttribute);
            string subType = streamAttributes.GetEntryIgnoreCase(SubTypeAttribute);

            return type.EnumTryParse(true, out result) && result == StreamType.Text
                   && AllowedCaptionStreamSubTypes.Any(i => string.Equals(i, subType, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}