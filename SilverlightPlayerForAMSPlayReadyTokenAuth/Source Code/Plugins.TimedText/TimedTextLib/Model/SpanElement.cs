// <copyright file="TimedText.cs" company="Microsoft Corporation">
// ===============================================================================
// MICROSOFT CONFIDENTIAL
// Microsoft Accessibility Business Unit
// Incubation Lab
// Project: Timed Text Library
// ===============================================================================
// Copyright 2009  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

using TimedText.Timing;
using TimedText.Formatting;
using System.Globalization;
namespace TimedText
{
    /// <summary>
    /// The span element functions as a logical container and a temporal
    /// structuring element for a sequence of textual content units having inline 
    /// level formatting semantics. The span element accepts as its children zero or 
    /// more elements in the Metadata.class element group, followed by zero or more 
    /// elements in the Animation.class element group, followed by zero or more br 
    /// element or text nodes interpreted as anonymous spans.
    /// 
    /// The span class also represents open text nodes, and thus has a Text property.
    /// When presented on a visual medium, a span element is intended to generate a 
    /// sequence of inline areas, each containing one or more glyph areas.
    /// </summary>
    public class SpanElement : TimedTextElementBase
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SpanElement(string text)
        {
            AnonymousSpanElement aSpan = new AnonymousSpanElement(text);
            Children.Add(aSpan);
            aSpan.Parent = this;
            TimeSemantics = TimeContainer.Par;
        }

        /// <summary>
        /// Constructor to set defaults
        /// </summary>
        public SpanElement()
        {
            TimeSemantics = TimeContainer.Par;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Return formatting object for span element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {
            if (TemporallyActive(tick))
            {
                var block = new Inline(this);

                foreach (var child in Children)
                {
                    if (child is BrElement || child is AnonymousSpanElement)
                    {
                        #region Add text to the Inline formatting object
                        var fo = (child as TimedTextElementBase).GetFormattingObject(tick);
                        if (fo != null)
                        {
                            fo.Parent = block;
                            block.Children.Add(fo);
                        }
                        #region copy metadata across to inline, since we want to use this
                        foreach (var d in this.Metadata)
                        {
                            if (!child.Metadata.ContainsKey(d.Key))
                            {
                                child.Metadata.Add(d.Key, d.Value);
                            }
                        }
                        #endregion
                        #endregion
                    }
                    else if (child is SpanElement)
                    {
                        #region flatten span hierarchy
                        var fo = (child as SpanElement).GetFormattingObject(tick);
                        if (fo != null)
                        {
                            /*
                            /// Flattened nested <span>A<span>B</span>C</span>
                            /// -> <Inline>A</Inline><Inline>B</Inline><Inline>C</Inline>
                            /// by hoisting out to outer context.
                            /// Hoisted elements will still inherit correctly, as style is inherited through
                            /// the Timed Text tree, not the formatting object tree.
                            /// something to watch out for when computing relative 
                            /// values though.
                             */
                            foreach (var nestedInline in fo.Children)
                            {
                                nestedInline.Parent = block;
                                block.Children.Add(nestedInline);
                            }
                        }
                        #endregion
                    }
                    if (child is SetElement)
                    {
                        #region Add animations to Inline
                        var fo = ((child as SetElement).GetFormattingObject(tick)) as Animation;
                        if (fo != null)
                        {
                            block.Animations.Add(fo);
                        }
                        #endregion
                    }
                }
                return block;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Validity
        /*
         <span
          begin = <timeExpression>
          dur = <timeExpression>
          end = <timeExpression>
          region = IDREF
          style = IDREFS
          timeContainer = (par|seq)
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {any attribute in TT Metadata namespace ...}
          {any attribute in TT Style namespace ...}
          {any attribute not in default or any TT namespace ...}>
          Content: Metadata.class*, Animation.class*, Inline.class*
        </span>
        */
        /// <summary>
        /// Check validity of span element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, true, true, true);

        }

        /// <summary>
        /// Check valisdity of span element content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Allow arbitrary metadata
            while ((child < Children.Count)
               && ((Children[child] is MetadataElement)
               || (Children[child] is Metadata.MetadataElement)
                //|| (Children[child] is anonymousSpan)
            ))
            {
                child++;
            }
            #endregion

            #region Allow arbitrary set elements (Animation class)
            while ((child < Children.Count)
               && ((Children[child] is SetElement)
                //|| (Children[child] is anonymousSpan)
             ))
            {
                child++;
            }
            #endregion

            #region Allow arbitrary span, br and PCDATA (Inline class)
            while ((child < Children.Count)
               && ((Children[child] is SpanElement)
               || (Children[child] is BrElement)
               || (Children[child] is AnonymousSpanElement)
            ))
            {
                child++;
            }
            #endregion

            #region Ensure no other element present
            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }
            #endregion

            #region Check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
            #endregion
        }
        #endregion
    }

}
