using System;
using System.Windows;

namespace Microsoft.SilverlightMediaFramework.Samples.Framework
{
    public class Sample
    {
        public UIElement _activeUI;

        public Sample(string name, Type uiElementType)
        {
            Name = name;
            UIElementType = uiElementType;
        }


        public string Name { get; set; }
        public Type UIElementType { get; set; }


        public string CSharpCode
        {
            get
            {
                var codeDisplay = _activeUI as ISupportCodeDisplay;
                if (codeDisplay == null)
                    return string.Empty;
                else
                    return codeDisplay.CSharpCode;
            }
        }

        public string XamlCode
        {
            get
            {
                var codeDisplay = _activeUI as ISupportCodeDisplay;
                if (codeDisplay == null)
                    return string.Empty;
                else
                    return codeDisplay.XamlCode;
            }
        }

        public string HtmlCode
        {
            get
            {
                var codeDisplay = _activeUI as ISupportHtmlDisplay;
                if (codeDisplay == null)
                    return string.Empty;
                else
                    return codeDisplay.HtmlCode;
            }
        }

        public string BlendInstructions
        {
            get
            {
                var codeDisplay = _activeUI as ISupportBlendInstructions;
                if (codeDisplay == null)
                    return string.Empty;
                else
                    return codeDisplay.BlendInstructions;
            }
        }

        public UIElement ActiveUI()
        {
            //if (_activeUI == null)
            //    _activeUI = (UIElement) Activator.CreateInstance(UIElementType);
            //return _activeUI;

            return (UIElement)Activator.CreateInstance(UIElementType);
        }
    }
}