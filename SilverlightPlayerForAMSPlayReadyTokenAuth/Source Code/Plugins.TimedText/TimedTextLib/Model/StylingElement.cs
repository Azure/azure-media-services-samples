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
    /// The styling element is a container element used to group styling matter, 
    /// including metadata that applies to styling matter.The styling element accepts 
    /// as its children zero or more elements in the Metadata.class element group, 
    /// followed by zero or more style elements.
    /// </summary>
    public class StylingElement : TimedTextElementBase
    {
        #region validity
        /*
       <styling
              xml:id = ID
              xml:lang = string
              xml:space = (default|preserve)
              {any attribute not in default or any TT namespace ...}>
              Content: Metadata.class*, style*
        </styling>
        */
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, false, false, false, false, false);
        }

        protected override void ValidElements()
        {
            int child = 0;

            while ((child < Children.Count)
                && ((Children[child] is MetadataElement)
                || (Children[child] is Metadata.MetadataElement)))
            {
                child++;
            }
            while ((child < Children.Count)
                && (Children[child] is StyleElement))
            {
                child++;
            }

            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }

            // now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
        }
        #endregion
    }
}
