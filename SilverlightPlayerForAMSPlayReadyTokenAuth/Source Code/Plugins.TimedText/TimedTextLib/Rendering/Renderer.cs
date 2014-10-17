using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media; // to do abstract away from Windows Color dependancy?

using TimedText.Styling;  
namespace TimedText.Rendering
{
    /// <summary>
    /// represents an abstract interface to a renderObject. Specific output engines
    /// implement this in order to draw the formatting object tree.
    /// </summary>
    public interface IRenderObject
    {
        #region utilities
        void Open();
        void Close();
        #endregion

        #region output

        /// <summary>
        /// clear the drawing surface to the specified color.
        /// </summary>
        void Clear(Color color);

        /// <summary>
        /// Set the drawing opacity
        /// </summary>
        /// <param name="level"></param>
        void SetOpacity(byte level);
 
            /// <summary>
        /// Draw a line in the specified colour from x to y
        /// </summary>
        /// <param name="pen"></param>
        void DrawLine(Color color, double startX, double startY, double endX, double endY);

        /// <summary>
        /// Draw a rectangle outline with specified pen, filled with specified brush
        /// from top left (x1,y1) to bottom right (x2,y2)
        /// </summary>
        /// <param name="pen">outline pen</param>
        /// <param name="brush">fill brush</param>
        /// <param name="x1">top left x coordinate</param>
        /// <param name="y1">top left y coordinate</param>
        /// <param name="x2">bottom right x coordinate</param>
        /// <param name="y2">bottom right y coordinate</param>
        // void DrawRectangle(Pen pen, Brush brush, double x1, double y1, double x2, double y2);
        void DrawRectangle(Color color, double startX, double startY, double endX, double endY);

        /// Draws a series of glyphs identified by the specified text and font.


        void DrawText(string text, Font font, Color fill, TextDecorationAttributeValue decoration, double startX, double startY, TimedText.Informatics.MetadataInformation data);

        void DrawOutlineText(string text, Font font, Color fill, TimedText.Styling.TextOutline outline, double startX, double startY, TimedText.Informatics.MetadataInformation data);

        Styling.Rectangle ComputeTextExtent(string text, Font font);

        #endregion

        #region clipping
        /// <summary>
        /// Set clipping rectangle.
        /// </summary>
        /// <param name="rect">clipping rectangle</param>
        void PushClip(Styling.Rectangle rectangle);

        /// <summary>
        /// Unset clipping rectangle
        /// </summary>
        void PopClip();

        void PushScroll(double horizontalDistance, double verticalDistance);

        #endregion

        #region state
        double Width();
        double Height();
        #endregion
    }

#if WantSimpleRendering   
    public class SimpleRendering :  IRenderObject
    {
        #region IRenderer Members
        StringBuilder sb;
        string m_output;

        public String Output
        {
            get
            {
                return m_output;
            }
        }

        public void Open()
        {
            m_output = "";
            sb = new StringBuilder();
        }

        public void Close()
        {
            m_output = sb.ToString();
        }

        public void Clear(Color color)
        {
            //
        }

        public void SetOpacity(byte level)
        {
        }


        public void DrawLine(Color color, double startX, double startY, double endX, double endY)
        {
            //Console.WriteLine("draw line");
        }

        public void DrawRectangle(Color color, double startX, double startY, double endX, double endY)
        {
            //Console.WriteLine("draw rectangle");
        }

        public void DrawOutlineText(string text, Font font, Color fill, TimedText.Styling.TextOutline outline, double startX, double startY, TimedText.Informatics.MetadataInformation data)
        {
            //Console.WriteLine(s);
            sb.Append(text);
        }


        public void DrawText(string text, Font font, Color fill, TextDecorationAttributeValue decoration, double startX, double startY, TimedText.Informatics.MetadataInformation data)
        {
            sb.Append(text);
        }

        public Styling.Rectangle ComputeTextExtent(string text, Font font)
        {
            return new Styling.Rectangle();
        }


        public void SetClip(Styling.Rectangle rectangle)
        {
           //
        }

        public void UnsetClip()
        {
           //
        }

        public void PushClip(TimedText.Styling.Rectangle rectangle)
        {
            // to do -- add a sub layoutHost with clip set, into which drawing will occur.
          }

        public void PopClip()
        {
         }

        public double Width() { return 0; }
        public double Height() { return 0; }


        public void PushScroll(double horizontalDistance, double verticalDistance)
        {
         }

        public void DoScroll(double fraction)
        {
        }
        #endregion
    }
#endif
    
}
