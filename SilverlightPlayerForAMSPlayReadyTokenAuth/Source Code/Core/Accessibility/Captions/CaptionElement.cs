using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using Microsoft.SilverlightMediaFramework.Core.Media;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core.Accessibility.Captions
{
    /// <summary>
    /// Represents a closed caption
    /// </summary>
    public class CaptionElement : TimedTextElement
    {
        public CaptionElement()
        {
            CaptionElementType = Captions.TimedTextElementType.Text;
        }

        public int Index { get; set; }

    }
}