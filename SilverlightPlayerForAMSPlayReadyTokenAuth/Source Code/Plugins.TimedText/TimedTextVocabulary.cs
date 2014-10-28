using System.Xml.Linq;

namespace Microsoft.SilverlightMediaFramework.Plugins.TimedText
{
    internal static class TimedTextVocabulary
    {
#if !WINDOWS_PHONE
        internal static class Namespaces
        {
            internal static readonly XName TimedText = "{http://www.w3.org/2006/10/ttaf1}"; //root namespace
            internal static readonly XName TimedTextParameter = "{http://www.w3.org/2006/10/ttaf1#parameter}";  //ttp:
            internal static readonly XName TimedTextStyling = "{http://www.w3.org/2006/10/ttaf1#styling}"; //tts:
            internal static readonly XName TimedTextMetadata = "{http://www.w3.org/2006/10/ttaf1#metadata}"; //ttm:
        }
#endif
        internal static class Elements
        {
            internal static readonly XName Paragraph = "{http://www.w3.org/2006/10/ttaf1}p";
            internal static readonly XName Styling = "{http://www.w3.org/2006/10/ttaf1}styling";
            internal static readonly XName Style = "{http://www.w3.org/2006/10/ttaf1}style";
        }

        internal static class Attributes
        {
            internal static readonly XName Id = "{http://www.w3.org/XML/1998/namespace}id";
            internal static readonly XName AllowSeek = "allowseek";  //Custom attribute used for TimelineMediaMarker.AllowSeek
            internal static readonly XName Begin = "begin";
            internal static readonly XName Duration = "dur";
            internal static readonly XName End = "end";
            internal static readonly XName Style = "style";

            internal static class Metadata
            {
                internal static readonly XName Role = "{http://www.w3.org/2006/10/ttaf1#metadata}role";
            }

            internal static class Styling
            {
                internal static readonly XName BackgroundColor = "{http://www.w3.org/2006/10/ttaf1#styling}backgroundColor";
                internal static readonly XName Color = "{http://www.w3.org/2006/10/ttaf1#styling}color";
                internal static readonly XName Display = "{http://www.w3.org/2006/10/ttaf1#styling}display";
                internal static readonly XName DisplayAlign = "{http://www.w3.org/2006/10/ttaf1#styling}displayAlign";
                internal static readonly XName Extent = "{http://www.w3.org/2006/10/ttaf1#styling}extent";
                internal static readonly XName FontFamily = "{http://www.w3.org/2006/10/ttaf1#styling}fontFamily";
                internal static readonly XName FontSize = "{http://www.w3.org/2006/10/ttaf1#styling}fontSize";
                internal static readonly XName FontStyle = "{http://www.w3.org/2006/10/ttaf1#styling}fontStyle";
                internal static readonly XName FontWeight = "{http://www.w3.org/2006/10/ttaf1#styling}fontWeight";
                internal static readonly XName LineHeight = "{http://www.w3.org/2006/10/ttaf1#styling}lineHeight";
                internal static readonly XName Opacity = "{http://www.w3.org/2006/10/ttaf1#styling}opacity";
                internal static readonly XName Origin = "{http://www.w3.org/2006/10/ttaf1#styling}origin";
                internal static readonly XName Overflow = "{http://www.w3.org/2006/10/ttaf1#styling}overflow";
                internal static readonly XName Padding = "{http://www.w3.org/2006/10/ttaf1#styling}padding";
                internal static readonly XName ShowBackground = "{http://www.w3.org/2006/10/ttaf1#styling}showBackground";
                internal static readonly XName TextAlign = "{http://www.w3.org/2006/10/ttaf1#styling}textAlign";
                internal static readonly XName Visibility = "{http://www.w3.org/2006/10/ttaf1#styling}visibility";
                internal static readonly XName WrapOption = "{http://www.w3.org/2006/10/ttaf1#styling}wrapOption";
                internal static readonly XName TextOutline = "{http://www.w3.org/2006/10/ttaf1#styling}textOutline";
                internal static readonly XName Direction = "{http://www.w3.org/2006/10/ttaf1#styling}direction";
                // Unsupported DFXP styles
                //internal static readonly XName DynamicFlow = "{http://www.w3.org/2006/10/ttaf1#styling}dynamicFlow";
                //internal static readonly XName TextDecoration = "{http://www.w3.org/2006/10/ttaf1#styling}textDecoration";
                //internal static readonly XName UnicodeBiki = "{http://www.w3.org/2006/10/ttaf1#styling}unicodeBiki";
                //internal static readonly XName WritingMode = "{http://www.w3.org/2006/10/ttaf1#styling}writingMode";
                //internal static readonly XName ZIndex = "{http://www.w3.org/2006/10/ttaf1#styling}zIndex";
            }
        }

        internal static class Roles
        {
            internal const string Caption = "caption";
            internal const string Timeline = "timeline";
            internal const string Description = "description";
        }

        internal static class TimeMetrics
        {
            internal const string Hours = "h";
            internal const string Minutes = "m";
            internal const string Seconds = "s";
            internal const string MilliSeconds = "ms";
            internal const string Frames = "f"; //Not Supported
            internal const string Ticks = "t";
        }
    }
}
