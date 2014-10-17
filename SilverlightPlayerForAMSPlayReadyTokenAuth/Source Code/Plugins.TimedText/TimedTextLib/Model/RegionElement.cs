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
    /// The region element is used to define a space or area into which content is to 
    /// be flowed for the purpose of presentation.The region element accepts as its 
    /// children zero or more elements in the Metadata.class element group, followed 
    /// by zero or more elements in the Animation.class element group, followed by zero 
    /// or more style elements.
    /// </summary>
    public class RegionElement : TimedTextElementBase
    {
        #region Constants
        /// <summary>
        /// Name for region if none is specified
        /// </summary>
        public static string DefaultRegionName
        {
            get
            {   // return a key that is not a legal XML ID.
                return "default region";
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RegionElement()
        {
            TimeSemantics = TimeContainer.Par;
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Return formatting object for region element
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {
            if (TemporallyActive(tick))
            {
                return new BlockContainer(this);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Validity
        /*
         <region
          begin = <timeExpression>
          dur = <timeExpression>
          end = <timeExpression>
          style = IDREFS
          timeContainer = (par|seq)
          xml:id = ID
          xml:lang = string
          xml:space = (default|preserve)
          {any attribute in TT Style namespace ...}
          {any attribute not in default or any TT namespace ...}>
          Content: Metadata.class*, Animation.class*, style*
        </region>
        */

        /// <summary>
        /// Check validity of region element attributes
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(false, true, true, true, false, true);
        }

        /// <summary>
        /// Check vlidity of region element content model
        /// </summary>
        protected override void ValidElements()
        {
            int child = 0;

            #region Allow arbitrary metadata
            while ((child < Children.Count)
               && ((Children[child] is MetadataElement)
               || (Children[child] is Metadata.MetadataElement)))
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

            #region Allow arbitrary style elements
            while ((child < Children.Count)
               && (Children[child] is StyleElement)
            )
            {
                StyleElement s = Children[child] as StyleElement;
                #region copy nested style attributes over as if they were inline
                foreach (var a in s.Attributes)
                {
                    // we should really check if its already defined, however
                    // by adding at the start we ensure the later (inline)
                    // style will override.
                    this.Attributes.Insert(0, a);
                }
                #endregion
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
