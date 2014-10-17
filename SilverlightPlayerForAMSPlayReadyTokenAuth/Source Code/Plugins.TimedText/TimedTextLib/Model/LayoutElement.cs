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
using System.Globalization;

namespace TimedText
{
    /// <summary>
    /// The layout element is a container element used to group layout matter, including
    /// metadata that applies to layout matter.The layout element accepts as its children
    /// zero or more elements in the Metadata.class element group, followed by zero or more
    /// region elements.
    /// </summary>
    public class LayoutElement : TimedTextElementBase
    {
        #region Validity
        /*
         <layout
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {any attribute not in default or any TT namespace ...}>
          Content: Metadata.class*, region*
        </layout>
        */
        /// <summary>
        /// Check validity of layout element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, false, false, false, false, false);
        }

        /// <summary>
        /// Check validity of layout element content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Allow arbitrary metadata
            while ((child < Children.Count) && ((Children[child] is MetadataElement)
               || (Children[child] is Metadata.MetadataElement)))
            {
                child++;
            }
            #endregion

            #region Allow arbitrary region elements
            while ((child < Children.Count) && (Children[child] is RegionElement))
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
