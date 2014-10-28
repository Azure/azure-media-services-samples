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
using System.Collections.Generic;
using TimedText.Formatting;
using TimedText.Timing;

namespace TimedText
{
    /// <summary>
    /// The tt element serves as the root, document element of a document 
    /// instance. The tt element accepts as its children zero or one head 
    /// element followed by zero or one body element.
    /// </summary>
    public class TtElement : TimedTextElementBase
    {
        #region Private variables
        HeadElement m_head;
        BodyElement m_body;
        #endregion

        #region map ID's to regions.

        private Dictionary<string, RegionElement> m_regions;

        public Dictionary<string, RegionElement> Regions
        {
            get
            {
                return m_regions;
            }
        }

        public override BodyElement Body
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;
            }
        }

        public HeadElement Head
        {
            get
            {
                return m_head;
            }
            set
            {
                m_head = value;
            }
        }        
        #endregion

        #region map ID's to agents.
        private Dictionary<string, Metadata.AgentElement> m_agents;
        public Dictionary<string, Metadata.AgentElement> Agents
        {
            get
            {
                return m_agents;
            }
        }
        #endregion

        #region map ID's to style. for referential styles
        private Dictionary<string, StyleElement> m_styles;
        public Dictionary<string, StyleElement> Styles
        {
            get
            {
                return m_styles;
            }
        }
        #endregion

        #region map parameter values
        private Dictionary<string, string> m_parameters;
        public Dictionary<string, string> Parameters
        {
            get
            {
                return m_parameters;
            }
        }
        #endregion

        #region Constructor
        public TtElement()
        {
            m_parameters = new Dictionary<string, string>();
            m_styles = new Dictionary<string, StyleElement>();
            m_agents = new Dictionary<string, Metadata.AgentElement>();
            m_regions = new Dictionary<string, RegionElement>();
        }
        #endregion

        #region Formatting
        /// <summary>
        /// return the root formatting object
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public override FormattingObject GetFormattingObject(TimeCode tick)
        {
            // if there is no body. then empty regions would be pruned
            // see 9.3.3.  part 5. map each non-empty region element to 
            // an fo:block-container element...
            if (m_body == null) return null;
            if (!m_body.TemporallyActive(tick)) return null;

            #region create single root and flow for the document.
            var root = new Formatting.Root(this);
            var flow = new Formatting.Flow(null);
            flow.Parent = root;
            root.Children.Add(flow);
            #endregion

            #region add a block container to the flow for each temporally active region
            foreach (var region in Regions.Values)
            {
                if (region.TemporallyActive(tick))
                {
                    var blockContainer = region.GetFormattingObject(tick);
                    #region apply animations on regions
                    foreach (var child in region.Children)
                    {
                        if (child is SetElement)
                        {
                            var fo = ((child as SetElement).GetFormattingObject(tick));
                            if (fo is Animation)
                            {
                                blockContainer.Animations.Add(fo as Animation);
                            }
                        }
                    }
                    #endregion

                    blockContainer.Parent = flow;
                    flow.Children.Add(blockContainer);

                    #region create a new subtree for the body element
                    /// select it into this region by adding its children 
                    /// to block container
                    var block = m_body.GetFormattingObject(tick);
                    if (block != null)
                    {
                        block.Prune(region.Id);  // deselect any content not for this region
                        if (block.Children.Count > 0)
                        {
                            if (block.Children[0].Children.Count > 0)
                            {
                                blockContainer.Children.Add(block);
                                block.Parent = blockContainer;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion
            return root;
        }
        #endregion

        #region validity
        /*
         <tt
            tts:extent = string
            xml:id = ID
            xml:lang = string     (required)
            xml:space = (default|preserve) : default
            {any attribute in TT Parameter namespace ...}
            {any attribute not in default or any TT namespace ...}>
            Content: head?, body?
        </tt>
         */

        /// <summary>
        /// Check tt element attribute validity
        /// </summary>
        protected override void ValidAttributes()
        {
            ValidateAttributes(true, false, false, false, false, false);

            if (Language == null)
            {
                Error("TT element must specify xml:lang attribute ");
            }
        }

        /// <summary>
        /// Check tt element validity
        /// </summary>
        protected override void ValidElements()
        {
            bool isValid = true;
            // we need an extra check to validate the root attributes in order
            // to ensure parameters are parsed.
            ValidAttributes();

            #region check this elements model
            switch (Children.Count)
            {
                case 0:
                    return;
                case 1:
                    {
                        #region test if child element is head or body
                        if (Children[0] is HeadElement)
                        {
                            m_head = Children[0] as HeadElement;
                            isValid = true;
                        }
                        else if (Children[0] is BodyElement)
                        {
                            m_body = Children[0] as BodyElement;
                            m_head = new HeadElement();
                            Children.Clear();
                            Children.Add(m_head);
                            Children.Add(m_body);
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region Check first child is head, and second is body
                        if (Children[0] is HeadElement)
                        {
                            m_head = Children[0] as HeadElement;
                        }
                        if (Children[1] is BodyElement)
                        {
                            m_body = Children[1] as BodyElement;
                        }
                        else
                        {
                            isValid = (m_body != null && m_head != null);
                        }
                        #endregion
                    }
                    break;
                default:
                    {
                        #region Cannot be valid
                        isValid = false;
                        #endregion
                    }
                    break;
            }
            #endregion

            if (!isValid)
            {
                Error("erroneous child in " + this.ToString());
            }

            #region now check each of the children is individually valid
            foreach (TimedTextElementBase element in Children)
            {
                element.Valid();
            }
            #endregion

            #region Add default region if none was specified
            if (isValid && Regions.Count < 1)
            {
                LayoutElement defaultLayout = new LayoutElement();
                defaultLayout.LocalName = "layout";
                defaultLayout.Namespace = m_head.Namespace;

                m_head.Children.Add(defaultLayout);
                defaultLayout.Parent = m_head;
                RegionElement defaultRegion = new RegionElement();
                defaultRegion.SetLocalStyle("backgroundColor", "black");
                defaultRegion.SetLocalStyle("color", "white");
                defaultLayout.Children.Add(defaultRegion);
                defaultRegion.Parent = defaultLayout;
                defaultRegion.Id = RegionElement.DefaultRegionName;
                Root.Regions[defaultRegion.Id] = defaultRegion;
            }
            #endregion

        }
        #endregion

    }
}
