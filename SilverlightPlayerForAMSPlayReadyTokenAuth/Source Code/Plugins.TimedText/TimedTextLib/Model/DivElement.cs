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
using TimedText.Formatting;
using TimedText.Timing;
using System.Globalization;

namespace TimedText
{
    /// <summary>
    /// The div element functions as a logical container and a temporal structuring 
    /// element for a sequence of textual content units represented as logical paragraphs.
    /// The div element accepts as its children zero or more elements in the Metadata.class
    /// element group, followed by zero or more elements in the Animation.class element
    /// group, followed by zero or more p elements.
    /// </summary>
    public class DivElement : TimedTextElementBase
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DivElement()
        {
            TimeSemantics = TimeContainer.Par;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Get the formatting object for this element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {

            if (TemporallyActive(tick))
            {
                var block = new Block(this);

                foreach (var child in Children)
                {
                    if (child is DivElement || child is PElement)
                    {
                        var fo = (child as TimedTextElementBase).GetFormattingObject(tick);
                        if (fo != null)
                        {
                            fo.Parent = block;
                            block.Children.Add(fo);
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
        <div
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
          Content: Metadata.class*, Animation.class*, Block.class*
        </div>
        */
        /// <summary>
        /// Check validity of div element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, true, true, true);
        }

        /// <summary>
        /// Check validity of dive element content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Allow arbitrary metadata
            while ((child < Children.Count)
               && ((Children[child] is MetadataElement)
               || (Children[child] is Metadata.MetadataElement)
            ))
            {
                child++;
            }
            #endregion

            #region Allow arbitrary set elements (Animation class)
            while ((child < Children.Count)
               && ((Children[child] is SetElement)
            ))
            {
                child++;
            }
            #endregion

            #region allow arbitrary div, p elements (Block class)
            while ((child < Children.Count)
                && ((Children[child] is DivElement)
                || (Children[child] is PElement)
                //|| (Children[child] is anonymousSpan)
            ))
            {
                child++;
            }
            #endregion

            #region Ensure no other elements present
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
