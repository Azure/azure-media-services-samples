using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using TimedText.Rendering;
using TimedText.Styling;
using TimedText.Timing;
using System.Collections.ObjectModel;

/// This set of classes implements a subset of the XSL:FO semantics sufficient
/// to render timed text documents

namespace TimedText.Formatting
{
    /// <summary>
    /// Formatter is a function which takes a rendering engine 
    /// and applies it to the element
    /// </summary>
    /// <param name="renderObject"></param>
    public delegate void Formatter(Rendering.IRenderObject renderObject);

 
    #region Formatting Object base class
    /// <summary>
    /// This is the base class for formatting object. We re-use the TimeTree base class, but
    /// its timing is not relevant here
    /// </summary>
    public class FormattingObject : Timing.TimeTree<FormattingObject, TimedTextAttributeBase>.TreeType
    {
        Collection<Animation> m_animations = new Collection<Animation>();

        private FontSize m_ContextualFontSize;

        public FontSize ContextualFontSize
        {
            get { return m_ContextualFontSize; }
            set { m_ContextualFontSize = value; }
        }

        /// <summary>
        /// Get the region for this formatting object
        /// </summary>
        /// <returns>id of the region to format into</returns>
        public string RegionId
        {
            get
            {
                /// region isnt a style property per se, but it behaves a bit like one.
                /// so thats where we store it
                return (string)m_element.GetComputedStyle("region", null);
            }
        }


        #region Style Properties
        public TextOutline TextOutlineStyleProperty
        {
            get
            {
                TextOutline outline = (TextOutline)m_element.GetComputedStyle("textOutline", AssignedRegion);
                return outline;
            }
        }

        //public FontStyleAttribute FontStyleStyleProperty
        //{
        //    get
        //    {
        //        FontStyleAttribute fontStyle = (FontStyleAttribute)m_element.GetComputedStyle("fontStyle", AssignedRegion);
        //        return fontStyle;
        //    }
        //}

        //public FontWeightAttribute FontWeightStyleProperty
        //{
        //    get
        //    {
        //        FontWeightAttribute fontWeight = (FontWeightAttribute)m_element.GetComputedStyle("fontWeight", AssignedRegion);
        //        return fontWeight;
        //    }
        //}

        //public string FontFamilyStyleProperty
        //{
        //    get
        //    {
        //        string fontFamily = (string)m_element.GetComputedStyle("fontFamily", AssignedRegion);
        //        return fontFamily;
        //    }
        //}

        public string ShowBackgroundStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("showBackground", null);
            }
        }

        public int RenderPassProperty
        {
            get
            {
                return (int)m_element.GetComputedStyle("#renderpass", AssignedRegion);
            }
        }

        public double TopStyleProperty
        {
            get
            {
                return (double)m_element.GetComputedStyle("#top", AssignedRegion);
            }
        }

        public double LeftStyleProperty
        {
            get
            {
                return (double)m_element.GetComputedStyle("#left", AssignedRegion);
            }
        }

        public bool PreserveStyleProperty
        {
            get
            {
                return (bool)m_element.GetComputedStyle("#preserve", null);
            }
        }
 
        public Color ColorStyleProperty
        {
            get
            {
                var property = m_element.GetComputedStyle("color", AssignedRegion);
                Color c = property != null ? (Color)property : Color.FromArgb(255, 00, 00, 00);
                return c;
            }
        }

        public Color BackgroundColorStyleProperty { get { return GetBackgroundColorStyle(); } }

        private Color GetBackgroundColorStyle()
        {
            object e;

            // anonymous spans do inherit if they.
            if (m_element is AnonymousSpanElement)
            {
                SpanElement parent = m_element.Parent as SpanElement;
                if (parent != null)
                {
                    e = parent.GetReferentStyle("backgroundColor");
                }
                else
                {
                    e = Colors.Transparent;
                }
            }
            else
            {
                e = m_element.GetReferentStyle("backgroundColor");
            }

            if(e is Inherit)
                e = m_element.GetComputedStyle("backgroundColor", AssignedRegion);
            
            if (e != null && e is Color)
                return (Color)e;
            else
                return Colors.Transparent;
        }

        public string DisplayStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("display", AssignedRegion);
            }
        }

        public string DirectionStyleProperty
        {
            get
            {
                string direction = (string)m_element.GetComputedStyle("direction", AssignedRegion);
                if (direction == "auto")
                {
                    // then author hasnt set it, defer to writing mode
                    WritingMode mode = WritingModeStyleProperty;
                    switch (mode)
                    {
                        case WritingMode.RightLeftTopBottom:
                            return "rtl";
                        default:
                            return "ltr";
                    }
                }
                else
                {
                    return direction;
                }
            }
        }

        public string UnicodeBidirectionStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("unicodeBidi", AssignedRegion);
            }
        }

        public string DisplayAlignStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("displayAlign", AssignedRegion);
            }
        }

        public double OpacityStyleProperty
        {
            get
            {
                return (double)m_element.GetComputedStyle("opacity", AssignedRegion);
            }
        }

        public string TextAlignStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("textAlign", AssignedRegion);
            }
        }

        public TextDecorationAttributeValue TextDecorationStyleProperty
        {
            get
            {
                return (TextDecorationAttributeValue)m_element.GetComputedStyle("textDecoration", AssignedRegion);
            }
        }

        public WritingMode WritingModeStyleProperty
        {
            get
            {   // lrtb | rltb | tbrl | tblr | lr | rl | tb
                switch ((string)m_element.GetComputedStyle("writingMode", AssignedRegion))
                {
                    case "lrtb":
                    case "lr":
                        return WritingMode.LeftRightTopBottom;
                    case "rltb":
                    case "rl":
                        return WritingMode.RightLeftTopBottom;
                    case "tbrl":
                    case "tb":
                        return WritingMode.TopBottomRightLeft;
                    case "tblr":
                        return WritingMode.TopBottomLeftRight;
                    default:
                        return WritingMode.LeftRightTopBottom;
                }
            }
        }

        public string VisibilityStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("visibility", AssignedRegion);
            }
        }

        public string OverflowStyleProperty
        {
            get
            {
                return (string)m_element.GetComputedStyle("overflow", AssignedRegion);
            }
        }

       public bool WrapOptionStyleProperty
       {
           get
           {
               bool wrap;
               string wrapOption = (string)Element.GetComputedStyle("wrapOption", AssignedRegion);
               wrap = wrapOption == "wrap";
               return wrap;
           }
       }

#endregion

        #region Context sensitive style properties
       public LineHeight CalculateLineHeightStyle(Rendering.IRenderObject renderObject)
       {
           LineHeight lineHeight = (LineHeight)m_element.GetComputedStyle("lineHeight", AssignedRegion);
           if (lineHeight is NormalHeight)
           {
               if (this.Children.Count == 0)
               {
                   lineHeight = new LineHeight(m_ContextualFontSize.FontHeight);
               }
               else
               {
                   lineHeight = MaxChildFontSize(renderObject);
               }
           }
           else
           {
               lineHeight.SetContext(renderObject.Width(), renderObject.Height());
               lineHeight.SetFontContext(m_ContextualFontSize.FontWidth, m_ContextualFontSize.FontHeight);
           }
           return lineHeight;
       }
       
        public Origin CalculateOriginStyle(Rendering.IRenderObject renderObject)
        {
            Origin origin = m_element.GetComputedStyle("origin", AssignedRegion) as Origin;
            if (origin as AutoOrigin != null)
            {
                origin = new Origin(0, 0);  // renderObject origin?
            }
            else
            {
                origin.SetContext(renderObject.Width(), renderObject.Height());
                origin.SetFontContext(m_ContextualFontSize.FontWidth, m_ContextualFontSize.FontHeight);
            }
            return origin;
        }
 
        public PaddingThickness CalculatePaddingStyle(Rendering.IRenderObject renderObject)
        {
            PaddingThickness pad = (PaddingThickness)m_element.GetComputedStyle("padding", AssignedRegion);
            pad.SetContext(renderObject.Width(), renderObject.Height());
            pad.SetFontContext(m_ContextualFontSize.FontWidth, m_ContextualFontSize.FontHeight);
            return pad;
        }
 
        public Extent CalculateExtentStyle(Rendering.IRenderObject renderObject)
        {
            Extent extent = m_element.GetComputedStyle("extent", AssignedRegion) as Extent;
            if (extent as AutoExtent != null)
            {
                extent = new Extent(renderObject.Width(), renderObject.Height());
            }
            else
            {
                extent.SetContext(renderObject.Width(), renderObject.Height());
                extent.SetFontContext(m_ContextualFontSize.FontWidth, m_ContextualFontSize.FontHeight);
            }
            return extent;
        }      
  
       public FontSize CalculateFontSizeStyle(Rendering.IRenderObject renderObject)
        {
            string fontSizeSpec = (string)m_element.GetComputedStyle("fontSize", AssignedRegion);
            FontSize fontSize = new FontSize(fontSizeSpec, m_ContextualFontSize, renderObject.Width(), renderObject.Height());
            return fontSize;
        }

 
         
         //public double GetOffsetStyle()
        //{
        //    return (double)m_element.GetComputedStyle("#offset", GetRegion());
        //}
        //public double GetInlineDirectionMarginStyle()
        //{
        //    double inlineDirectionMargin = (double)m_element.GetComputedStyle("#inlineDirectionMargin", GetRegion());

        //    return inlineDirectionMargin;
        //}
        //public double GetBlockDirectionMarginStyle()
        //{
        //    double blockDirectionMargin = (double)m_element.GetComputedStyle("#blockDirectionMargin", GetRegion());
        //    return blockDirectionMargin;
        //}

        #endregion

         /// <summary>
         /// Remove any subtrees which are not selected into the region.
         /// </summary>
         /// <param name="regionId">region to select</param>
        public void Prune(string regionId)
        {
            TreeType[] unprunedChildren = new TreeType[Children.Count];
            Children.CopyTo(unprunedChildren, 0);
            //Children = new List<Timing.TimeTree<FormattingObject,TimedTextAttribute>>();
            //Children = new TimeTreeCollection<FormattingObject,TimedTextAttribute>.();
            Children.Clear();

            foreach (var child in unprunedChildren)
            {
                FormattingObject fo = child as FormattingObject;
                fo.Prune(regionId);
                string foRegionID = fo.RegionId;
                if ((regionId == "default region") || (string.IsNullOrEmpty(foRegionID) && fo.Children.Count > 0) || (foRegionID == regionId))
                {
                    Children.Add(fo);
                }
            }
        }
        
         /// <summary>
         /// Calculate the largest fontsize of children. 
         /// If no children, use context font.
         /// </summary>
         /// <param name="renderObject"></param>
         /// <returns></returns>
        private LineHeight MaxChildFontSize(IRenderObject renderObject)
        {
            double fontHeight = 0;
            foreach (var child in Children)
            {
                FormattingObject fo = child as FormattingObject;
                string fontSizeSpec = (string)fo.Element.GetComputedStyle("fontSize", AssignedRegion);
                FontSize fontSize = new FontSize(fontSizeSpec, m_ContextualFontSize, renderObject.Width(), renderObject.Height());
                fontHeight = Math.Max(fontHeight, fontSize.FontHeight);
            }
            return new LineHeight(fontHeight);
        }

         /// <summary>
         /// Calculate the context font size.
         /// </summary>
         /// <param name="renderObject"></param>
        public void ComputeRelativeStyles(IRenderObject renderObject)
        {
            if (Parent == null || this is Root || m_element == null)
            {
                m_ContextualFontSize = new FontSize("1c", null, renderObject.Width(), renderObject.Height());
            }
            else
            {
                FormattingObject fo = Parent as FormattingObject;
                FontSize contextFontSize = fo.m_ContextualFontSize;
                string fontSize = (string)m_element.GetReferentStyle("fontSize");
                if (fontSize == null)
                {
                    m_ContextualFontSize = contextFontSize;
                }
                else
                {
                    m_ContextualFontSize = new FontSize(fontSize, contextFontSize, renderObject.Width(), renderObject.Height());
                }
            }
        }

         /// <summary>
         /// Store here the list of <set/> animations that are in effect.
         /// </summary>
        public Collection<Animation> Animations
        {
            get
            {
                return m_animations;
            }
        }

         /// <summary>
         /// reference to the underlying timed text element
         /// </summary>
        private TimedTextElementBase m_element;

        public TimedTextElementBase Element
        {
            get { return m_element; }
            set { m_element = value; }
        }

         /// <summary>
         /// public access to the underlying timed text element
         /// </summary>
        //public TimedTextElement Elementx
        //{
        //    get
        //    {
        //        return m_element;
        //    }
        //}

         /// <summary>
         /// Return the currently assigned region for this Formatting object
         /// </summary>
         /// <returns></returns>
        public RegionElement AssignedRegion
        {
            get
            {
                if (this is BlockContainer)
                {
                    return (this).Element as RegionElement;
                }
                else if (Parent != null)
                {
                    return ((FormattingObject)Parent).AssignedRegion;
                }
                return null;
            }
        }

        /// <summary>
        /// Retun a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public virtual Formatter CreateFormatter()
        {
            return (IRenderObject renderObject) =>
            {
                foreach (var child in Children)
                {
                    FormattingObject fmt = child as FormattingObject;
                    fmt.CreateFormatter()(renderObject);
                }
                return;
            };
        }

        /// <summary>
        /// Override animated attributes.
        /// </summary>
        public void ApplyAnimations()
        {
            bool setOnce = false;  // only record the first old value, in case multiple aanimations are applied
            foreach (var animation in Animations)
            {
                foreach (var attribute in animation.Element.Attributes)
                {
                    if (attribute.IsStyleAttribute())
                    {
                        string property = attribute.LocalName;
                        object newvalue = animation.Element.GetReferentStyle(property);
                        object oldValue = this.Element.GetReferentStyle(property);
                        if (oldValue != null && !setOnce)
                        {
                            this.Element.SetLocalStyle("#_" + property, oldValue);
                            setOnce = true;
                        }
                        this.Element.SetLocalStyle(property, newvalue);
                    }
                }
            }
        }

        /// <summary>
        /// Undo all the animated overrides.
        /// </summary>
        public void RemoveAppliedAnimations()
        {
            foreach (var animation in Animations)
            {
                foreach (var attribute in animation.Element.Attributes)
                {
                    if (attribute.IsStyleAttribute())
                    {
                        string property = attribute.LocalName;
                        object oldValue = this.Element.GetReferentStyle("#_" + property);
                        if (oldValue != null)
                        {
                            this.Element.SetLocalStyle(property, oldValue);
                        }
                        else
                        {
                            this.Element.ClearLocalStyle(property);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The reference area is where this element computes its dimensions from
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle ReferenceArea()
        {
            return new Rectangle();
        }

        //private Rectangle m_actualDrawing = new Rectangle();

        // ought to make this a property, but its parts are set outside here.
        protected Rectangle ActualDrawing = new Rectangle();
 
        /// <summary>
        /// The ActualArea area is where this element draws itself into
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle ActualArea()
        {
            return ActualDrawing;
        }

         /// <summary>
         /// return the formatting object which represents the timed text tree at 
         /// the given time
         /// </summary>
         /// <param name="timeSpan"></param>
         /// <param name="tree"></param>
         /// <returns></returns>
        public static FormattingObject RenderTree(TimeCode timeSpan, TimedTextElementBase tree)
        {
            
            FormattingObject root = tree.Root.GetFormattingObject(timeSpan);
            return root;
        }

         /// <summary>
         /// Set a local style on the element
         /// </summary>
         /// <param name="property"></param>
         /// <param name="value"></param>
        public void SetStyle(string property, object value)
        {
            m_element.SetLocalStyle(property, value);
        }

         /// <summary>
         /// clear a local style
         /// </summary>
         /// <param name="property"></param>
        public void ClearStyle(string property)
        {
            m_element.ClearLocalStyle(property);
        }

    }
    #endregion

    #region Root formatting object
     /// <summary>
    /// block elements that contain block container
    /// </summary>
    public class Root : FormattingObject
    {
        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        public Root(TimedTextElementBase element)
        {
            Element = element;
        }

        /// <summary>
        /// Retun a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                Extent e = new Extent(renderObject.Width(), renderObject.Height());               
                Element.SetLocalStyle("extent", e);

                renderObject.Clear(ColorStyleProperty);
                
                foreach (var child in Children)
                {
                    FormattingObject fmt = child as TimedText.Formatting.FormattingObject;
                    fmt.CreateFormatter()(renderObject);
                }
                return;
            };
        }
    }
     #endregion

    #region Flow formatting object
    /// <summary>
    /// Flow element
    /// </summary>
    public class Flow : FormattingObject
    {
        public Flow(TimedTextElementBase element)
        {
            Element = element;
        }

        /// <summary>
        /// Return a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                List<BlockContainer> regions = new List<BlockContainer>();
                foreach (var child in Children)
                {
                    BlockContainer block = child as TimedText.Formatting.BlockContainer;
                    block.ApplyAnimations();
                    regions.Add(block);
                }
                regions.Sort(new ZOrdering());
                foreach (var child in regions)
                {
                    child.CreateFormatter()(renderObject);
                    child.RemoveAppliedAnimations();
                }
                return;
            };
        }
    }
    #endregion

    #region BlockContainer formatting object

    public class ZOrdering : IComparer<BlockContainer>
    {
        public int Compare(BlockContainer x, BlockContainer y)
        {
            if (x.ZIndex < y.ZIndex) return 1;
            if (x.ZIndex < y.ZIndex) return -1;
            return 0;
        }
    }

    /// <summary>
    ///  block elements that contain block content and produce reference area
    /// </summary>
    public class BlockContainer : FormattingObject
    {
        public double ZIndex
        {
            get
            { 
                object val = Element.GetComputedStyle("zIndex", null);
                if (val is double)
                {
                    return (double)val;                  
                }
                return (0);       
            }    
        }

        public BlockContainer(TimedTextElementBase element)
        {
            Element = element;
        }

        Rectangle referenceArea;
        Rectangle drawnArea;

        /// <summary>
        /// Block containers generate a non null reference area.
        /// </summary>
        /// <returns></returns>
        public override Rectangle ReferenceArea()
        {
            return referenceArea;
        }

        /// <summary>
        /// Block containers generate a non null reference area.
        /// </summary>
        /// <returns></returns>
        public override Rectangle ActualArea()
        {
            return drawnArea;
        }

        /// <summary>
        /// Retun a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                referenceArea = new Rectangle();
                drawnArea = new Rectangle();

                if (ShowBackgroundStyleProperty != "always")
                {
                    bool needToShow = false;
                    foreach (var child in Children)
                    {
                        if (child.Children.Count > 0)
                        {
                            needToShow = true;
                            break;
                        }
                    }
                    if (!needToShow) return;
                }
                
                string visible = VisibilityStyleProperty;
                string display = DisplayStyleProperty;
                double opacity = OpacityStyleProperty;

                Color backgroundColor = BackgroundColorStyleProperty;
                PaddingThickness pad = CalculatePaddingStyle(renderObject);
                Origin origin = CalculateOriginStyle(renderObject);
                Extent extent = CalculateExtentStyle(renderObject);
                string clip = OverflowStyleProperty;

                Rectangle bounds = new Rectangle();
                bounds.X = origin.X;
                bounds.Y = origin.Y;
                bounds.Width = extent.Width;
                bounds.Height = extent.Height;

                this.referenceArea.X = origin.X + pad.WidthStart;
                this.referenceArea.Y = origin.Y + pad.WidthBefore;
                this.referenceArea.Height = extent.Height - (pad.WidthAfter + pad.WidthBefore);
                this.referenceArea.Width = extent.Width - (pad.WidthEnd + pad.WidthStart);

 
                if (visible != "hidden" && display != "none")
                {
                    // we do layout twice. Once without making any marks, but calculating the actual drawing sizes
                    // the second to do the actual rendering.
                    for (int pass = 0; pass < 2; pass++)
                    {
                        if (pass > 0)
                        {
                            // we need to clip twice here. First time round to include the padding area
                            if (clip == "hidden" || clip == "scroll")
                            {  
                                renderObject.PushClip(bounds);
 
                            }

                            if (0.0 <= opacity && opacity < 1.0)
                            {   // hack - set opacity here so its the outer clip context that gets it.
                                byte level = (byte)(255 * opacity);
                                renderObject.SetOpacity(level);
                            }
                            renderObject.DrawRectangle(backgroundColor, origin.X, origin.Y, origin.X + extent.Width, origin.Y + extent.Height);
                            // clip for second time to content (after drawing background so padding gets drawn).
                            if (clip == "hidden" || clip == "scroll")
                            {  
                                renderObject.PushClip(this.referenceArea);
                            }
//                            renderObject.DrawRectangle(backgroundColor, origin.X, origin.Y, origin.X + extent.Width, origin.Y + extent.Height);
                        }
                        double top = this.referenceArea.Y;
                        double left = this.referenceArea.X;
                        double scrollHeight = 1;  // blank in and blank out.
                        //double scrollDuration = 1;

                        foreach (var child in Children)
                        {
                            // a block container only has one child - the block.
                            // from CSS 14.2 - body should set the background of the 'canvas'
                            // which is what the region is.
                            // so pick up the block background here.
                            Block fmt = child as TimedText.Formatting.Block;
                            renderObject.DrawRectangle(fmt.BackgroundColorStyleProperty, origin.X, origin.Y, origin.X + extent.Width, origin.Y + extent.Height);

                            #region compute display align
                            /// To do - generalise to block progression direction
                            double leftover = ReferenceArea().Height - fmt.ActualArea().Height;  // stacked height of all the children.
                            double offset = 0;

                            switch (DisplayAlignStyleProperty)
                            {
                                case "center":
                                    offset = (leftover / 2);
                                    break;
                                case "after":
                                    offset = leftover;
                                    break;
                            }
                            top += offset;

                            #endregion

                            fmt.SetStyle("#left", left);
                            fmt.SetStyle("#top", top);
                            fmt.SetStyle("#renderpass", pass);
                            fmt.CreateFormatter()(renderObject);
                            top += fmt.ActualArea().Height;
                            scrollHeight = fmt.ActualArea().Height - this.referenceArea.Height;
                            if (clip == "scroll" && scrollHeight > 0)
                            {
                                renderObject.PushScroll(0, -scrollHeight);
                            }
                        }
                        if (clip == "hidden" || clip == "scroll")
                        {

                             renderObject.PopClip();
                            renderObject.PopClip();
                        }
 
                    }
                }

                return;
            };
        }
    }
    #endregion

    #region Block formatting object
    /// <summary>
    ///  block elements that contain block content
    /// </summary>
    public class Block : FormattingObject
    {
        public Block(TimedTextElementBase element)
        {
            Element = element;
        }

        public override Rectangle ReferenceArea()
        {
            FormattingObject fo = this.Parent as FormattingObject;

            return fo.ReferenceArea();
        }

        public override Rectangle ActualArea()
        {
            return ActualDrawing;
        }

        /// <summary>
        /// Return a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                ApplyAnimations();
                
                if (DisplayStyleProperty != "none")  // kill layout of this subtree.
                {
                    double top = TopStyleProperty;
                    double left = LeftStyleProperty;

                    Color backgroundColor = BackgroundColorStyleProperty;
                    if (this.Parent is BlockContainer)
                    {
                        // then we already drew the background
                        backgroundColor = Colors.Transparent;
                    }

                    int pass = RenderPassProperty;

                    if (VisibilityStyleProperty == "hidden")  // everything is drawn transparently.
                    {
                        backgroundColor = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                    }

                    if (pass == 0)
                    {

                        ActualDrawing = new Rectangle();
                        ActualDrawing.X = ReferenceArea().X;
                        ActualDrawing.Y = ReferenceArea().Y;
                        ActualDrawing.Width = ReferenceArea().Width;
                        ActualDrawing.Height = 0;
                        foreach (var child in Children)
                        {
                            FormattingObject fmt = child as TimedText.Formatting.FormattingObject;
                            /// blocks stack their children inside the 
                            /// reference area. But in order to know where we have to
                            /// know how big they are.
                            fmt.SetStyle("#left", left);
                            fmt.SetStyle("#top", top);
                            fmt.CreateFormatter()(renderObject);
                            top += fmt.ActualArea().Height;
                            ActualDrawing.Height += fmt.ActualArea().Height;
                        }
                    }
                    else
                    {
                        ActualDrawing.Y = top;
                        ActualDrawing.X = left;
                        double right = Math.Min(ActualDrawing.X + ActualDrawing.Width, ReferenceArea().X + ReferenceArea().Width);
                        double bottom = Math.Min(ActualDrawing.Y + ActualDrawing.Height, ReferenceArea().Y + ReferenceArea().Height);
                        renderObject.DrawRectangle(backgroundColor, ActualDrawing.X, ActualDrawing.Y, right, bottom);
 
                        WritingMode mode = WritingModeStyleProperty;
                        bool vertical = mode == WritingMode.TopBottomLeftRight || mode == WritingMode.TopBottomRightLeft;
                        bool rightMode = mode == WritingMode.TopBottomRightLeft;

                        // set left depending on writing mode.
                        if (vertical && rightMode)
                        {   // start over on the right.
                            left = ActualDrawing.X + ActualDrawing.Width;
                        }
                        else
                        {   // start on the left.
                            left = ActualDrawing.X;
                        }

                        foreach (var child in Children)
                        {
                            /// issue - do we need some block advance width?
                            FormattingObject fmt = child as TimedText.Formatting.FormattingObject;
                            if (vertical && rightMode) left -= fmt.ActualArea().Width;
                            fmt.SetStyle("#top", top);
                            fmt.SetStyle("#left", left);
                            fmt.CreateFormatter()(renderObject);
                            if(!vertical) top += fmt.ActualArea().Height;
                            if (vertical && !rightMode) left += fmt.ActualArea().Width;
                        }
                    }
                }
                RemoveAppliedAnimations();
                return;
            };
        }


    }
    #endregion

    #region Paragraph formatting object
    /// <summary>
    ///  block elements that contain inline content
    /// </summary>
    public class Paragraph : FormattingObject
    {
        /// <summary>
        /// Temporary roll up of all the text in the paragraph
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// collect up the Unicode Bidi algorithm on this paragraph
        /// </summary>
        /// <param name="unicodeBidirection">one of embed, normal, bidiOveride</param>
        /// <param name="direction">one of ltr or rtl</param>
        public static string AddBidirectionEncoding(string text, string unicodeBidirection, string direction)
        {
            string data = "";
            switch (unicodeBidirection)
            {
                case "undefined":
                case "embed":
                    //The direction of this embedding level is given by the 'direction'
                    //property. Inside the element, reordering is done implicitly. 
                    //This corresponds to adding a LRE (U+202A; for 'direction: ltr') 
                    //or RLE (U+202B; for 'direction: rtl') at the start of the 
                    //element and a PDF (U+202C) at the end of the element. 
                    if (direction == "ltr")
                    {
                        data = "\u202A" + text + "\u202C";
                    }
                    else
                    {
                        data = "\u202B" + text + "\u202C";
                    }
                    break;
                case "bidiOverride":
                    ///reordering is strictly in sequence according to the 'direction' 
                    ///property; the implicit part of the bidirectional algorithm 
                    ///is ignored. This corresponds to adding a LRO (U+202D; for 
                    ///'direction: ltr') or RLO (U+202E; for 'direction: rtl') at 
                    ///the start of the element and a PDF (U+202C) at the end
                    ///of the element. 
                    if (direction == "ltr")
                    {
                        data = "\u202D" + text + "\u202C";
                    }
                    else
                    {
                        data = "\u202E" + text + "\u202C";
                    } break;
                default:
                    data = text;
                    break;

            }
            return data;
        }

        public Paragraph(TimedTextElementBase element)
        {
            Element = element;
        }

        public override Rectangle ReferenceArea()
        {
            FormattingObject fo = this.Parent as FormattingObject;

            return fo.ReferenceArea();
        }

        /// <summary>
        /// Return a formatting function for paragraphs.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            /// children of Paragraph should be InlineContent, all Inlines should have 
            /// been lifted by this point.
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                ApplyAnimations();
                char[] breakChars = new char[] { '\n' };

                if (DisplayStyleProperty != "none")  // kill layout of this subtree.
                {
                    #region display
                    #region setup
                    WritingMode mode = WritingModeStyleProperty;
                    bool vertical = mode == WritingMode.TopBottomLeftRight || mode == WritingMode.TopBottomRightLeft;
                    bool horizontal = !vertical;
                    //bool rightMode = mode == WritingMode.TopBottomRightLeft;
                    
                    LineHeight lineHeight = CalculateLineHeightStyle(renderObject);
                    FontSize fontSize = CalculateFontSizeStyle(renderObject);
                    double top = TopStyleProperty;
                    double left = LeftStyleProperty;
                    Color backgroundColor = BackgroundColorStyleProperty;
                    if (VisibilityStyleProperty == "hidden")  // everything is drawn transparently.
                    {
                        backgroundColor = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                    }

                    bool wrap;
                    double layoutRight = ReferenceArea().X + ReferenceArea().Width;
                    #endregion


                    if (RenderPassProperty == 0)
                    {
                        #region pass 0
                        #region Compute intial reference rectangle
                        ActualDrawing = new Rectangle();
                        // set the area we will fill
                        if (horizontal)
                        {
                            ActualDrawing.X = ReferenceArea().X;
                            ActualDrawing.Y = ReferenceArea().Y;
                            ActualDrawing.Height = fontSize.FontHeight;
                            ActualDrawing.Width = ReferenceArea().Width;
                        }
                        else
                        {
                            ActualDrawing.X = ReferenceArea().X;
                            ActualDrawing.Y = ReferenceArea().Y;
                            ActualDrawing.Height = ReferenceArea().Height;
                            ActualDrawing.Width = fontSize.FontWidth;;
                        }
                        #endregion

                        double lineLength = 0;
                        double lengthSinceBreak = 0,
                               length = 0,
                               lengthV = 0;

                        foreach (var inlineContent in Children)
                        {
                            /// In the first pass we need to know how big the 
                            /// paragraph is, and where the left margins are.
                            InlineContent content = inlineContent as InlineContent;
                            bool isPreserve = content.PreserveStyleProperty;
                            Font font = content.GetFont(renderObject);

                            #region figure out where line breaks go and communicate them into pass 2,

                            if (!isPreserve) content.ClearBreaks();  // remove any breaks in the content
                            int currentChar = 0; // where we are in the local text.
                            int lastbreakChar = -1;

                            double wrappingSpace = ActualDrawing.Width;
                            if (!horizontal) wrappingSpace = ActualDrawing.Height;

                            wrap = content.WrapOptionStyleProperty;
                            var characters = content.Content.ToCharArray();
                            Collection<int> breaks = new Collection<int>();
                            if (content != null)
                            {
                                foreach (var c in characters)
                                {
                                    content.MeasureText(c.ToString(), renderObject, font, ref length, ref lengthV);
                                    if (!horizontal) length = lengthV;

                                    bool shouldWrap = wrap && ((lineLength + length) > wrappingSpace);
                                    bool hardBreak = (content.Element is BrElement) || (isPreserve && breakChars.Contains(c));
                                    if (Unicode.BreakOpportunities.Contains(c)) // other breakable chars?
                                    {
                                        lastbreakChar = currentChar;
                                        lengthSinceBreak = 0;
                                    }
                                    if (hardBreak || shouldWrap)
                                    {
                                        #region break line if we can.
                                        if (lastbreakChar > 0)
                                        {   // break at the previous convenient spot.
                                            if (!isPreserve)
                                            {
                                                breaks.Add(lastbreakChar);
                                            }
                                            lineLength = lengthSinceBreak;
                                            lastbreakChar = -1;
                                        }
                                        if (hardBreak)
                                        {
                                            if (!isPreserve)
                                            {
                                                breaks.Add(currentChar);
                                            }
                                            lineLength = 0;

                                            lengthSinceBreak = 0;
                                        }
                                        else
                                        {
                                            // cant break yet. we'll overflow to the next space
                                            currentChar++;
                                            lineLength += length;
                                            if (lastbreakChar > 0) lengthSinceBreak += length;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        currentChar++;
                                        lineLength += length;
                                        if (lastbreakChar > 0) lengthSinceBreak += length;
                                    }
                                }
                            }
                            if (horizontal)
                            {
                                ActualDrawing.Height += (lineHeight.Height * breaks.Count);
                            }
                            else
                            { // line width?
                                ActualDrawing.Width += (lineHeight.Width * breaks.Count);
                            }
                            content.InsertBreaks(breaks);
                           #endregion
                         }
                        #endregion
                    }
                    else
                    {
                        #region pass 1

                        #region Draw Background
                        // in the second pass we render the inline content.
                        ActualDrawing.Y = top;
                        ActualDrawing.X = left;
                        // in case we overflowed, we dont want to draw backround over padding.
                        double right = Math.Min(ActualDrawing.X + ActualDrawing.Width, ReferenceArea().X + ReferenceArea().Width);
                        double bottom = Math.Min(ActualDrawing.Y + ActualDrawing.Height, ReferenceArea().Y + ReferenceArea().Height);
                        renderObject.DrawRectangle(backgroundColor, ActualDrawing.X, ActualDrawing.Y, right, bottom);
                        //renderObject.DrawRectangle(backgroundColor, m_actualDrawing.X, m_actualDrawing.Y, right, bottom);
                        #endregion

                        #region Setup
                        double lineLength = 0;
                        double leftMargin = ActualDrawing.X;
                        double rightMargin = ActualDrawing.X + ActualDrawing.Width;
                        double topMargin = ActualDrawing.Y;
                        double x_loc = -1;
                        double y_loc = -1; // m_actualDrawing.Y;
                        bool breakLine = true;
                        bool suppressWhitespace = true;

                        if (horizontal)
                        {
                            x_loc = -1;
                            y_loc = topMargin;
                        }
                        else
                        {
                            if (mode == WritingMode.TopBottomLeftRight)
                            {
                                x_loc = leftMargin;
                                y_loc = -1;
                            }
                            else
                            {
                                x_loc = rightMargin - lineHeight.Width;
                                y_loc = -1;
                            }
                        }
                        #endregion
 
                        #region fill the text buffer
                        System.Text.StringBuilder alltext = new System.Text.StringBuilder();
                        foreach (var inlineContent in Children)
                        {
                            InlineContent content = inlineContent as InlineContent;
                            bool isPreserve = content.PreserveStyleProperty;

                            // BUG - when we bidi the whole paragraph in one go, which
                            // is  the proper way, we screw up the 
                            // styling information. So for now we bidi in discrete lumps, which is wrong
                            // but close enough for today.
                            // 
                            if (isPreserve)
                            {
                                alltext.Append(content.Content);
                            }
                            else
                            {
                                alltext.Append(content.Content);

//                                string bidi = AddBidirectionEncoding(content.Content, content.UnicodeBidirectionStyleProperty, content.DirectionStyleProperty);
//                                alltext.Append(NBidi.NBidi.LogicalToVisual(bidi));
                            }
                        }

                        string paragraphText = alltext.ToString();
 
                        #endregion
                       
                        int charCount = 0;  // index into the combined text above.
                        foreach (var inlineContent in Children)
                        {                        
                            // bug - we need to collapse consecutive spaces beteen inlines.
                            InlineContent content = inlineContent as InlineContent;

                            content.ApplyAnimations();
                            double rtlWidth = 0, rtlHeight = 0;
                            Font font = content.GetFont(renderObject);
                            content.MeasureText(content.Content, renderObject, font, ref rtlWidth, ref rtlHeight);
                            
                            if (content.DisplayStyleProperty != "none")
                            {
                                #region display content
                                wrap = content.WrapOptionStyleProperty;
                                bool isPreserve = content.PreserveStyleProperty;

                                if (isPreserve) suppressWhitespace = false;

                                #region continue reposition after inline
                                if (!breakLine && mode == WritingMode.RightLeftTopBottom)
                                {
                                    x_loc -= content.ActualArea().Width;
                                }
                                #endregion


                                if (content != null)
                                {
                                    var characters = content.Content.ToCharArray();
                                    for (int i = 0; i < content.Content.Length; i++)
                                    {
                                        // if the bidi algorithm elides characters this should hopefully catch it.
                                        if (charCount >= paragraphText.Length) break;
                                        char c = paragraphText[charCount];
                                        double advanceHeight = 0;
                                        double advanceWidth = 0;
                                        content.MeasureText(c.ToString(), renderObject, font, ref advanceWidth, ref advanceHeight);
                                        bool softBreak = wrap && (!(content.Element is BrElement) && breakChars.Contains(c));
                                        bool hardBreak = (content.Element is BrElement) || (isPreserve && breakChars.Contains(c));
                                        if (softBreak || hardBreak)  // soft breaks are inserted in pass 1.
                                        {
                                            #region Apply Line break
                                            if (horizontal)
                                            {
                                                y_loc += lineHeight.Height;
                                            }
                                            else
                                            {
                                                if (mode == WritingMode.TopBottomLeftRight)
                                                {
                                                    x_loc += lineHeight.Width; 
                                                }
                                                else
                                                {
                                                    x_loc -= lineHeight.Width;
                                                }
                                             }
                                            #endregion
                                            breakLine = true;
                                            if (!isPreserve) suppressWhitespace = true;
                                            charCount++;
                                        }
                                        else
                                        {
                                            if (breakLine)
                                            {
                                                breakLine = false;
                                                #region Initialise new line
                                                if (mode == WritingMode.LeftRightTopBottom)
                                                {
                                                    x_loc = leftMargin + ComputeLeftMargin(renderObject, content, font, ActualDrawing.Width, paragraphText, charCount);
                                                }
                                                else if (mode == WritingMode.RightLeftTopBottom)
                                                {
                                                    layoutRight = rightMargin;
                                                    x_loc = layoutRight - rtlWidth; // -ComputeLeftMargin(renderObject, content, font, m_actualDrawing.Width, paragraphText, charCount);
                                                } else
                                                {
                                                    y_loc = top + ComputeLeftMargin(renderObject, content, font, ActualDrawing.Height, paragraphText, charCount);
                                                }
                                                #endregion
                                            }
                                            #region Render this character
                                            if (horizontal)
                                            {
                                                if (!(suppressWhitespace && Unicode.Whitespace.Contains(c)))
                                                {
                                                    renderObject.DrawRectangle(content.BackgroundColorStyleProperty, x_loc, y_loc, x_loc + advanceWidth, y_loc + font.EmHeight);
                                                    RenderChar(x_loc, y_loc, font, renderObject, content, c);
                                                    x_loc += advanceWidth;
                                                    lineLength += advanceWidth;
                                                    if (Unicode.Whitespace.Contains(c) && !isPreserve) suppressWhitespace = true;
                                                    else suppressWhitespace = false;
                                                }
                                            }
                                            else
                                            {
                                                if (!(suppressWhitespace && Unicode.Whitespace.Contains(c)))
                                                {
                                                    renderObject.DrawRectangle(content.BackgroundColorStyleProperty, x_loc, y_loc, x_loc + advanceHeight, y_loc + font.EmHeight); // bug - Em width...
                                                    RenderChar(x_loc, y_loc, font, renderObject, content, c);
                                                    y_loc += advanceHeight;
                                                    lineLength += advanceHeight;
                                                    if (Unicode.Whitespace.Contains(c) && !isPreserve) suppressWhitespace = true;
                                                    else suppressWhitespace = false;
                                                }
                                            }
                                            charCount++;
                                            #endregion
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                charCount += content.Content.Length;
                            }
                            #region begin reposition after inline
                            if (mode == WritingMode.RightLeftTopBottom)
                            {
                                x_loc -= rtlWidth;
                            }
                            #endregion
                            content.RemoveAppliedAnimations();
                        }
                        #endregion
                    }
                    #endregion
                }
                 
                RemoveAppliedAnimations();
                return;
            };
        }

        private static void RenderChar(double x_loc, double y_loc, Font font, IRenderObject renderObject, InlineContent content, char c)
        {
            TextOutline outline = content.TextOutlineStyleProperty;
            Color fg = content.ColorStyleProperty;
            // Color bg = content.BackgroundColorStyleProperty;

            if (content.VisibilityStyleProperty == "hidden")
            {
                fg = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                // bg = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            }
            string sh = c.ToString();
            TimedText.Informatics.MetadataInformation data = new TimedText.Informatics.MetadataInformation();
            data.Role = content.Element.GetMetadata("role");

            if (outline.Width > 0 || outline.Blur > 0)
            {
                renderObject.DrawOutlineText(sh, font, fg, outline, x_loc, y_loc, data);
            }
            else
            {
                renderObject.DrawText(sh, font, fg, content.TextDecorationStyleProperty, x_loc, y_loc, data);
            }

            return;
        }

        /// <summary>
        /// Find the length of the next rendered line.
        /// </summary>
        /// <param name="renderObject"></param>
        /// <param name="content"></param>
        /// <param name="font"></param>
        /// <param name="width"></param>
        /// <param name="characters"></param>
        /// <param name="startchar"></param>
        /// <returns></returns>
        private static double ComputeLeftMargin(IRenderObject renderObject, InlineContent content, Font font, double width, string characters, int startchar)
        {
            string direction = content.DirectionStyleProperty;
            WritingMode mode = content.WritingModeStyleProperty;
            double margin = width,
                   marginL = 0,
                   marginT = 0;
            int i = startchar;
            int j = characters.IndexOf('\n', i);
            string substring;
            if (j > i)
            {
                substring = characters.Substring(i, j - i);
            }
            else
            {
                substring = characters.Substring(i);
            }
            
            content.MeasureText(substring, renderObject, font, ref marginL, ref marginT);

            if (mode == WritingMode.TopBottomLeftRight || mode == WritingMode.TopBottomRightLeft)
            {
                margin -= marginT;
            }
            else
            {
                margin -= marginL;
            }
            i++;

            switch (content.TextAlignStyleProperty)
            {
                case "center":
                    margin = margin / 2;
                    break;
                case "start":
                case "left":
                    if (direction == "ltr")
                    {
                        margin = 0;
                    }
                    break;
                case "end":
                case "right":
                    if (direction == "rtl" || mode == WritingMode.RightLeftTopBottom)
                    {
                        margin = 0;
                    }
                    break;
            }
            return margin;
        }

        public override Rectangle ActualArea()
        {
            return ActualDrawing;
        }
    }
    #endregion

    #region Inline formatting object
    /// <summary>
    /// inline elements that contain inline content. These are temporary
    /// artifacts caused by <span> elements and get squeezed out
    /// </summary>
    public class Inline : FormattingObject
    {
        public Inline(TimedTextElementBase element)
        {
            Element = element;
        }

        /// <summary>
        /// Retun a formatting function for this element.
        /// This should not be called, as we will take care of it in Paragraph.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return null;
         }
    }
    #endregion

    #region Animation formatting object
    /// <summary>
    /// Animation elements wrap the set object; they are attached elements
    /// Their formatter is never actually called.
    /// </summary>
    public class Animation : FormattingObject
    {
        public Animation(TimedTextElementBase element)
        {
            Element = element;
        }

        /// <summary>
        /// Retun a formatting function for this element.
        /// </summary>
        /// <returns></returns>
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                ComputeRelativeStyles(renderObject);
                foreach (var child in Children)
                {
                    FormattingObject fmt = child as TimedText.Formatting.FormattingObject;
                    fmt.CreateFormatter()(renderObject);
                }
                return;
            };
        }
    }
    #endregion

    #region InlineContent formatting object
    /// <summary>
    /// inline elements that contain inline content
    /// </summary>
    public class InlineContent : FormattingObject
    {
        private string m_original_text;

        public String Content
        {
            get
            {
                AnonymousSpanElement e = Element as AnonymousSpanElement;
                return e.Text;
            }
        }

        /// <summary>
        /// clear all breaks in the text.
        /// </summary>
        public void ClearBreaks()
        {
            if (Element is BrElement) return;  // dont clean explicit breaks;
            if (Element is AnonymousSpanElement)
            {
                AnonymousSpanElement e = Element as AnonymousSpanElement;
                e.Text = m_original_text;
            }
        }
        /// <summary>
        /// Insert breaks in the text at given positions.
        /// </summary>
        public void InsertBreaks(Collection<int> breaks)
        {
            if (Element is BrElement) return;  // dont touch explicit breaks;
            if (Element is AnonymousSpanElement)
            {
                AnonymousSpanElement e = Element as AnonymousSpanElement;
                int offset = 0;
                foreach (var n in breaks)
                {
                    string newtext = e.Text;

                    // insert a newline
                    if (Unicode.BreakOpportunities.Contains(newtext[n + offset]))
                    {
                        // replace the breaking char with a newline. 
                        if (Unicode.VisibleBreakChar.Contains(newtext[n + offset]))
                        {
                            // we should leave it in place if its a visible breaking opp.                           
                            newtext = newtext.Remove(n + offset, 1).Insert(n + offset, newtext[n + offset]+"\n");
                            offset++;
                        }
                        else
                        {
                            newtext = newtext.Remove(n + offset, 1).Insert(n + offset, "\n");
                        }
                    }
                    e.Text = newtext;
                }
            }
        }



        public InlineContent(TimedTextElementBase element)
        {
            Element = element;
            m_original_text = Content;
        }

        /// <summary>
        /// Return a formatting function for this element.
        /// </summary>
        /// <returns></returns>
 
        public override Formatter CreateFormatter()
        {
            return (Rendering.IRenderObject renderObject) =>
            {
                #region old code - this renderObject is not called any more.
                //ComputeRelativeStyles(renderObject);
                //ApplyAnimations();
                ////double blockDirectionMargin = GetBlockDirectionMarginStyle();
                ////double inlineDirectionMargin = GetInlineDirectionMarginStyle();
                ////double offset = 0;
                //string fontFamily = GetFontFamilyStyle();    
                //FontSize fontSize = GetFontSizeStyle(renderObject);
                //Font.WeightAttribute fontWeight = GetFontWeightStyle();
                //Font.StyleAttribute fontStyle = GetFontStyleStyle();
                //TextOutline outline = GetTextOutlineStyle();
                //string direction = GetDirectionStyle();

                ////LineHeight lineHeight = GetLineHeightStyle(renderObject);


                //if (GetDisplayStyle() != "none")
                //{
                //    Font font = new Font(fontFamily, fontSize.FontHeight, fontWeight, fontStyle);
                //    string text = (this.m_element as anonymousSpan).Text;
                //    Color c = GetColorStyle();
                //    Color bg = GetBackgroundColorStyle();
                //    if (GetVisibilityStyle() == "hidden")
                //    {
                //        c = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                //        bg = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
                //    }
                //    if (GetRenderPass() > 0)
                //    {
                //        renderObject.DrawRectangle(bg, m_actualDrawing.X, m_actualDrawing.Y, m_actualDrawing.X + m_actualDrawing.Width + 1, m_actualDrawing.Y + m_actualDrawing.Height - 2);
                //        TimedText.Informatics.MetaData data = new TimedText.Informatics.MetaData();
                //        data.Role = (string)m_element.GetMetadata("role");
                //        if (outline.Width > 0)
                //        {
                //            renderObject.DrawOutlineText(text, font, c, outline, m_actualDrawing.X, m_actualDrawing.Y, data);
                //        }
                //        else
                //        {
                //            double advance = 0;
                //            foreach (char ch in BidiVisualOrder)
                //            {  
                //                string sh = ch.ToString();
                //                renderObject.DrawText(sh, font, c, GetTextDecorationStyle(), m_actualDrawing.X + advance, m_actualDrawing.Y, data);
                //                advance += renderObject.ComputeTextExtent(sh, font).Width;
                //            }
                //        }
                //    }
                // }
                //UnApplyAnimations();
                #endregion
                return;
            };
        }

        public override Rectangle ActualArea()
        {
            return ActualDrawing;
        }

        /// <summary>
        /// Measure the width of content. 
        /// </summary>
        /// <param name="renderObject"></param>
        /// <returns></returns>
        internal void MeasureText(string measureText, IRenderObject renderObject, Font font, ref double width, ref double height)
        {
            Rectangle area = renderObject.ComputeTextExtent(measureText, font);
            ActualDrawing.Width = area.Width;
            ActualDrawing.Height = area.Height;
            width = ActualDrawing.Width;
            height = ActualDrawing.Height;
        }

        public Font GetFont(IRenderObject renderObject)
        {
            ComputeRelativeStyles(renderObject);
            string fontFamily = (string)Element.GetComputedStyle("fontFamily", AssignedRegion);
            string fontSizeSpec = (string)Element.GetComputedStyle("fontSize", AssignedRegion);
            FontSize fontSize = new FontSize(fontSizeSpec, ContextualFontSize, renderObject.Width(), renderObject.Height());
            FontWeightAttributeValue fontWeight = (FontWeightAttributeValue)Element.GetComputedStyle("fontWeight", AssignedRegion);
            FontStyleAttributeValue fontStyle = (FontStyleAttributeValue)Element.GetComputedStyle("fontStyle", AssignedRegion);
            Font font = new Font(fontFamily, fontSize.FontHeight, fontWeight, fontStyle);
            return font;
        }
    }
    #endregion
}
