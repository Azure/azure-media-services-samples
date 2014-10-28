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
#region Using
using System.Globalization;

#endregion

namespace TimedText
{
    public class MetadataElement : TimedTextElementBase
    {
        #region Validity
        /*
        <metadata
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {any attribute in TT Metadata namespace ...}
          {any attribute not in default or any TT namespace ...}>
          Content: {any element not in TT namespace}*
        </metadata>
        */
        /// <summary>
        /// Check validity of metadata element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, false, false, false, false);
        }

        /// <summary>
        /// Check validity of metadata content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Ensure the children are not tt elements.
            while ((child < Children.Count) &&
                    ( 
                      Children[child] is Metadata.MetadataElement ||
                     !(Children[child] is TimedTextElementBase))
                    )
            {
                child++;
            }
            #endregion

            #region Ensure no other elements are present
            if (Children.Count != child)
            {
                Error(Children[child].ToString() + " is not allowed in " + this.ToString() + " at position " + child.ToString(CultureInfo.CurrentCulture));
            }
            #endregion

            // can't check the validity of other children as they are not TT
        }
        #endregion
    }
}

