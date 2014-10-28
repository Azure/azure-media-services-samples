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
    /// The br element denotes an explicit line break. This is a specialised
    /// form of span containing a Unicode break character
    /// </summary>
    public class BrElement : AnonymousSpanElement
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BrElement()
            : base("\n")
        {
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Return formatting object for br element
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

        #region validity
        /*
         <br
          style = IDREFS
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {any attribute in TT Metadata namespace ...}
          {any attribute in TT Style namespace ...}
          {any attribute not in default or any TT namespace ...}>
          Content: Metadata.class*, Animation.class*
        </br>
        */

        /// <summary>
        /// Check valisity of br element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, false, false, false);
        }

        /// <summary>
        /// chack validity of br element content model
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

            #region Allow arbitrary set element (Animation class)
            while ((child < Children.Count)
               && (Children[child] is SetElement))
            {
                child++;
            }
            #endregion

            #region Ensure no other element is present
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
