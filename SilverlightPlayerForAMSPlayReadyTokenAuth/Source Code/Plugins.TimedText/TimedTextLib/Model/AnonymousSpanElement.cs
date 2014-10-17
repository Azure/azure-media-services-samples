
using TimedText.Timing;
using TimedText.Formatting;
using System.Xml;
namespace TimedText
{
    /// <summary>
    /// These are called out as a subclass of span, 
    /// as their validity is different than pure spans. 
    /// But generally they behave like spans.
    /// </summary>
    public class AnonymousSpanElement : SpanElement
    {
        #region Text property
        private string m_text = "";
        /// <summary>
        /// The PCDATA text of the span
        /// </summary>
        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }
        #endregion

        #region Bidi order property
        /// <summary>
        /// Reordered version of the text applying the bidi rules
        /// </summary>
        //public System.Collections.ObjectModel.Collection<char> BidiVisualOrder
        //{
        //    get;
        //    set;
        //}
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AnonymousSpanElement(string text)
        {
            m_text = text;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Return the formatting object for anonymous span element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {
            if (TemporallyActive(tick))
            {
                return new InlineContent(this);
            }
            return null;
        }
        #endregion

        public override void WriteElement(XmlWriter writer)
        {
            writer.WriteString(this.Text);
        }

    }
}
