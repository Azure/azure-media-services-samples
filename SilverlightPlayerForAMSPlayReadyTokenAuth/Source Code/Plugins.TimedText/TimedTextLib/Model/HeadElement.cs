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
    /// The head element is a container element used to group header matter,
    /// including metadata, styling, and layout information.
    /// The head element accepts as its children zero or more elements in the 
    /// Metadata.class element group, followed by zero or one styling element, 
    /// followed by zero or one layout element.
    /// </summary>
    public class HeadElement : TimedTextElementBase
    {

        #region validity
        /*
         <head
            xml:id = ID
            xml:lang = string
            xml:space = (default|preserve)>
            Content: Metadata.class*,  Parameters.class*, styling?, layout?
         * 
         * what does lang mean here? 
         * why are foreign and metadata attributes not allowed?
         </head>
         */
        /// <summary>
        /// Check the attributes of the timed text head element
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, false, false, false, false, false);
        }

        /// <summary>
        /// Check the validity of the timed text head element
        /// </summary>
        protected override void ValidElements()
        {
            bool isValid = true;
            int child = 0;

            #region allow artibtrary metadata
            while ((child < Children.Count)
                && ((Children[child] is MetadataElement)
                || (Children[child] is Metadata.MetadataElement)
            ))
            {
                child++;
            }
            #endregion

            #region Allow an arbitrary number of profile elements
            while ((child < Children.Count)
                && ((Children[child] is Parameter.ProfileElement)
            ))
            {
                child++;
            }
            #endregion

            #region Allow an optional styling and optional layout element
            if (child < Children.Count)
            {
                if (Children[child] is StylingElement)
                {
                    if (Children.Count == (child + 1))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = (Children[child + 1] is LayoutElement) && (Children.Count == (child + 2));
                    }
                }
                else if (Children[child] is LayoutElement)
                {
                    isValid = (Children.Count == (child + 1));
                }
                else
                {
                    Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
                }
            }
            #endregion

            if (!isValid)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }

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
