using System;
using Microsoft.SilverlightMediaFramework.Logging;

namespace Microsoft.SilverlightMediaFramework.Plugins.Monitoring.Logs
{
    /// <summary>
    /// Contains the raw trace log information received from the SSME
    /// </summary>
    public class TraceLog : Log
    {
        public TraceLog()
            : base(VideoLogTypes.Trace)
        {
        }

        public string ClassName
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.TraceClassName);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.TraceClassName, value);
            }
        }

        public DateTime? Date
        {
            get
            {
                return GetValue<DateTime>(VideoLogAttributes.TraceDate);
            }
            set
            {
                SetValue<DateTime>(VideoLogAttributes.TraceDate, value);
            }
        }

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

        public string MethodName
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.TraceMethodName);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.TraceMethodName, value);
            }
        }

        public string Text
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.TraceText);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.TraceText, value);
            }
        }

        public int? ThreadId
        {
            get
            {
                return GetValue<int>(VideoLogAttributes.TraceThreadId);
            }
            set
            {
                SetValue<int>(VideoLogAttributes.TraceThreadId, value);
            }
        }

        public string TraceArea
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.TraceArea);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.TraceArea, value);
            }
        }

        public string TraceLevel
        {
            get
            {
                return GetRefValue<string>(VideoLogAttributes.TraceLevel);
            }
            set
            {
                SetRefValue<string>(VideoLogAttributes.TraceLevel, value);
            }
        }

        public override string ToString()
        {
            return string.Join("\t", new [] { 
                Date.Value.ToString(),
                ClassName,
                MethodName,
                TraceArea,
                TraceLevel,
                Text,
                ThreadId.Value.ToString(),
                MediaElementId
            });
        }
    }
}
