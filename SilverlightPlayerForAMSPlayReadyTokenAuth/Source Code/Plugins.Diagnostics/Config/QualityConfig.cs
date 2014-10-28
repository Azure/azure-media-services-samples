using System;
using System.Xml;

namespace Microsoft.SilverlightMediaFramework.Diagnostics
{
    /// <summary>
    /// Responsible for indicating which quality data should be tracked and aggregated. Used to filter data that will not be used at the earliest possible moment to improve performance.
    /// </summary>
    public class QualityConfig
    {
        public bool DroppedFrames { get; set; }
        public bool RenderedFrames { get; set; }
        public bool ProcessCpuLoad { get; set; }
        public bool SystemCpuLoad { get; set; }
        public bool Bitrate { get; set; }
        public bool BitrateMax { get; set; }
        public bool BitrateMaxDuration { get; set; }
        public bool PerceivedBandwidth { get; set; }
        public bool VideoBufferSize { get; set; }
        public bool AudioBufferSize { get; set; }
        public bool Buffering { get; set; }
        public bool BitrateChangeCount { get; set; }
        public bool VideoDownloadLatency { get; set; }
        public bool AudioDownloadLatency { get; set; }
        public bool DvrOperationCount { get; set; }
        public bool FullScreenChangeCount { get; set; }
        public bool HttpErrorCount { get; set; }

        public static QualityConfig Load(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            QualityConfig result = new QualityConfig();

            reader.GoToElement();
            if (!reader.IsEmptyElement)
            {
                reader.ReadStartElement();
                while (reader.GoToSibling())
                {
                    switch (reader.LocalName)
                    {
                        case "DroppedFrames":
                            result.DroppedFrames = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "RenderedFrames":
                            result.RenderedFrames = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "ProcessCPULoad":
                            result.ProcessCpuLoad = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "SystemCPULoad":
                            result.SystemCpuLoad = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "Bitrate":
                            result.Bitrate = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "BitrateMax":
                            result.BitrateMax = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "BitrateMaxDuration":
                            result.BitrateMaxDuration = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "PerceivedBandwidth":
                            result.PerceivedBandwidth = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "VideoBufferSize":
                            result.VideoBufferSize = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "AudioBufferSize":
                            result.AudioBufferSize = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "Buffering":
                            result.Buffering = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "BitrateChangeCount":
                            result.BitrateChangeCount = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "VideoDownloadLatency":
                            result.VideoDownloadLatency = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "AudioDownloadLatency":
                            result.AudioDownloadLatency = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "DvrOperationCount":
                            result.DvrOperationCount = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "FullScreenChangeCount":
                            result.FullScreenChangeCount = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;
                        case "HttpErrorCount":
                            result.HttpErrorCount = Convert.ToBoolean(reader.ReadElementContentAsInt());
                            break;

                        default:
                            reader.Skip();
                            break;
                    }
                }
                reader.ReadEndElement();
            }
            else
                reader.Skip();

            return result;
        }
    }
}
