using System;
using Microsoft.SilverlightMediaFramework.Logging;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// The simplest video related log that can be created. This usually serves as the base class for other logs but can be used by itself.
    /// </summary>
    public class SimpleVideoEventLog : Log
    {
        public SimpleVideoEventLog(string logType)
            : base(logType)
        {

        }

        /// <summary>
        /// The media element ID (also cooresponds to the SSME.Name property)
        /// </summary>
        public string MediaElementId
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.MediaElementId);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.MediaElementId, value);
            }
        }

        /// <summary>
        /// The position of the video in time (e.g. 14 minutes into the video)
        /// </summary>
        public TimeSpan? VideoPosition
        {
            get
            {
                return GetValue<TimeSpan>(VideoLogAttributes.VideoPosition);
            }
            set
            {
                SetValue<TimeSpan>(VideoLogAttributes.VideoPosition, value);
            }
        }

        /// <summary>
        /// The total duration that the video has been playing (minus the paused and stopped periods of time).
        /// </summary>
        public TimeSpan? VideoSessionDuration
        {
            get
            {
                return GetValue<TimeSpan>(VideoLogAttributes.VideoSessionDuration);
            }
            set
            {
                SetValue<TimeSpan>(VideoLogAttributes.VideoSessionDuration, value);
            }
        }

        /// <summary>
        /// A generated ID specific to the video and player session. Because it is generated locally, this will be different for each user.
        /// </summary>
        public Guid? VideoSessionId
        {
            get
            {
                return GetValue<Guid>(VideoLogAttributes.VideoSessionId);
            }
            set
            {
                SetValue<Guid>(VideoLogAttributes.VideoSessionId, value);
            }
        }

#if VIDEOID
        /// <summary>
        /// A generated ID specific to the video and player session. Because it is generated locally, this will be different for each user.
        /// </summary>
        public string VideoId
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.VideoId);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.VideoId, value);
            }
        }
#endif
    }
}
