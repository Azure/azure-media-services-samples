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
    /// The body element functions as a logical container and a temporal structuring 
    /// element for a sequence of textual content units represented as logical divisions.
    /// The body element accepts as its children zero or more elements in the 
    /// Metadata.class element group, followed by zero or more elements in the Animation.
    /// class element group, followed by zero or more div elements.
    /// </summary>
    public class BodyElement : TimedTextElementBase
    {
        #region Constructor
        /// <summary>
        /// Constructor to set defaults
        /// </summary>
        public BodyElement()
        {
            TimeSemantics = TimeContainer.Par;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Get formatting object for body element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {

            Block block = null;

            if (TemporallyActive(tick))
            {
                block = new Block(this);

                foreach (var child in Children)
                {
                    if (child is DivElement)
                    {
                        var fo = (child as DivElement).GetFormattingObject(tick);
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
                            // fo.Parent = block;
                            block.Animations.Add(fo);
                        }
                    }
                }
            }
            return block;
        }
        #endregion

        #region Validity
        /*
        <body
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
          Content: Metadata.class*, Animation.class*, div*
        </body>
        */

        /// <summary>
        /// Validate body element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, true, true, true);
        }

        /// <summary>
        /// Validate body element content model
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

            #region Allow arbitrary number of div elements
            while ((child < Children.Count)
               && ((Children[child] is DivElement)
                // || (Children[child] is anonymousSpan)
            ))
            {
                child++;
            }
            #endregion

            #region Ensure no other children
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
