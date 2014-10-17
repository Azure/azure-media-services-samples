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
    /// The set element is used as a child element of a content element in order to 
    /// express a discrete change of some style parameter value that applies over some 
    /// time interval.The set element accepts as its children zero or more elements in 
    /// the Metadata.class element group.
    /// </summary>
    public class SetElement : TimedTextElementBase
    {
        #region Formatting
        /// <summary>
        /// Return the formatting object for set element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {

            if (TemporallyActive(tick))
            {
                var animation = new Animation(this);
                return animation;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Validity
        /*
        <set
          begin = <timeExpression>
          dur = <timeExpression>
          end = <timeExpression>
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {a single attribute in TT Style or TT Style Extension namespace}
          {any attribute not in default or any TT namespace ...}>
          Content: Metadata.class*
        </set>
        */
        /// <summary>
        /// Check validity of set element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, false, true, true, false, false);
        }

        /// <summary>
        /// Check validity of set element content model
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
