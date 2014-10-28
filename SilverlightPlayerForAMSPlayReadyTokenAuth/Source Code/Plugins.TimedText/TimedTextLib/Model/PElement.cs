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
using System.Linq;
using System.Globalization;

namespace TimedText
{

    /// <summary>
    /// A p element represents a logical paragraph, serving as a transition between 
    /// block level and inline level formatting semantics.The p element accepts as its
    /// children zero or more elements in the Metadata.class element group, followed by 
    /// zero or more elements in the Animation.class element group, followed by zero or 
    /// more span element, br element, or text nodes interpreted as anonymous spans.
    /// </summary>
    public class PElement : TimedTextElementBase
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PElement()
        {
            TimeSemantics = TimeContainer.Par;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Return formatting object for p element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {

            if (TemporallyActive(tick))
            {

                var block = new Paragraph(this);

                foreach (var child in Children)
                {
                    if (child is BrElement || child is AnonymousSpanElement)
                    {
                        var fo = (child as TimedTextElementBase).GetFormattingObject(tick);
                        if (fo != null)
                        {
                            fo.Parent = block;
                            block.Children.Add(fo);
                            foreach (var d in this.Metadata)
                            {
                                if (!child.Metadata.ContainsKey(d.Key))
                                {
                                    child.Metadata.Add(d.Key, d.Value);
                                }
                            }
                        }
                    }
                    else if (child is SpanElement)
                    {
                        var fo = (child as SpanElement).GetFormattingObject(tick);
                        if (fo != null)
                        {
                            /// Flattened nested <span>A<span>B</span>C</span>
                            /// -> <Inline>A</Inline><Inline>B</Inline><Inline>C</Inline>
                            /// by hoisting out to outer context.
                            /// nested elements will still inherit correctly, as style is inherited 
                            /// throughthe tt_element tree, not the formatting object tree.
                            /// something to watch out for when computing relative values though.
                            foreach (var nestedInline in fo.Children)
                            {
                                nestedInline.Parent = block;
                                block.Children.Add(nestedInline);
                                // copy the childs animations into the grandchild
                                // - need to watch the order here.
                                // deepest animations should win, so they need to be last in the list. 
                                // we reverse the list on entry so they get inserted at the front in
                                // the right order.
                                fo.Animations.Reverse();  // these are getting discarded.
                                foreach (var animation in fo.Animations)
                                {
                                    Formatting.FormattingObject grandchild = nestedInline as Formatting.FormattingObject;
                                    grandchild.Animations.Insert(0, animation);
                                }
                            }

                        }
                    }
                    if (child is SetElement)
                    {
                        var fo = ((child as SetElement).GetFormattingObject(tick)) as Animation;
                        if (fo != null)
                        {
                            block.Animations.Add(fo);
                        }
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
        <p
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
        </p>
        */

        /// <summary>
        /// Check validity of p element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, true, true, true);
        }

        /// <summary>
        /// Check validity of p element content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Allow arbitrary metadata
            while ((child < Children.Count)
               && ((Children[child] is MetadataElement)
               || (Children[child] is Metadata.MetadataElement)
                //      || (Children[child] is anonymousSpan)
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

            #region Allow arbitrary span, br, PCDATA (Inline class)
            while ((child < Children.Count)
               && ((Children[child] is SpanElement)
               || (Children[child] is BrElement)
               || (Children[child] is AnonymousSpanElement)
            ))
            {
                child++;
            }
            #endregion

            #region Check no other element is present
            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }
            #endregion

            #region now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
            #endregion

        }
        #endregion
    }
}
