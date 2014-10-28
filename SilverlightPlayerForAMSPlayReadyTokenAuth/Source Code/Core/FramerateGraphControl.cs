using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    [TemplatePart(Name = FrameRateLabelName, Type = typeof(TextBlock))]
    [TemplatePart(Name = FrameRateGraphCanvasName, Type = typeof(Canvas))]
    public class FramerateGraphControl : Control
    {
        private const string FrameRateLabelName = "FrameRateLabel";
        private const string FrameRateGraphCanvasName = "FrameRateGraphCanvas";

        private object _threadSync = new object();
        private DispatcherTimer _updateTimer;
        private TextBlock _frameRateLabel;
        private Canvas _frameRateGraphCanvas;
       
        /// <summary>
        /// The desired height of this control
        /// </summary>
        private const int knDesiredControlHeight = 100;
        /// <summary>
        /// The desired width of this control
        /// </summary>
        private const int knDesiredControlWidth = 200;

        /// <summary>
        /// The maximum X value of graph lines.
        /// </summary>
        private const int knGraphLineMaxX = 190;

        /// <summary>
        /// The maximum Y value of the graph with (0,0) as the
        /// upper left corner.
        /// </summary>
        private const int knGraphLineMaxY = 15;

        /// <summary>
        /// The minimum X value of graph lines (essentially the starting
        /// value of the graph lines.
        /// </summary>
        private const int knGraphLineMinX = 30;

        /// <summary>
        /// The minimum Y value of the graph with (0,0) as the
        /// upper left corner.
        /// </summary>
        private const int knGraphLineMinY = 85;

        /// <summary>
        /// The maximum fps that this graph displays.
        /// </summary>
        private const double knMaxFps = 60.0;

        /// <summary>
        /// The brush we use to draw the graph lines
        /// </summary>
        private static SolidColorBrush kbrGraphLineBrush = new SolidColorBrush(Colors.Green);

        /// <summary>
        /// The thickness of the graph line
        /// </summary>
        private static double knGraphLineThickness = 2.5;

        /// <summary>
        /// The desired size of this control
        /// </summary>
        private static Size kszDesiredSize = new Size(knDesiredControlWidth, knDesiredControlHeight);

        /// <summary>
        /// The current graph line displaying the current frames per second
        /// </summary>
        private Line m_currentLine;

        /// <summary>
        /// The last fps value that we saw.
        /// </summary>
        private double m_lastFps = -1.0;

        #region ChartUpdateFrequency
        /// <summary>
        /// ChartUpdateFrequency DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ChartUpdateFrequencyProperty =
            DependencyProperty.Register("ChartUpdateFrequency", typeof(TimeSpan), typeof(FramerateGraphControl), new PropertyMetadata(TimeSpan.FromMilliseconds(70)));

        /// <summary>
        /// Gets or sets the frequency with which the charts are updated.
        /// </summary>
        public TimeSpan ChartUpdateFrequency
        {
            get { return (TimeSpan)GetValue(ChartUpdateFrequencyProperty); }
            set { SetValue(ChartUpdateFrequencyProperty, value); }
        }
        #endregion

        #region CurrentFramerate
        /// <summary>
        /// CurrentFramerate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty CurrentFramerateProperty =
            DependencyProperty.Register("CurrentFramerate", typeof(double), typeof(FramerateGraphControl), new PropertyMetadata((double)0));

        /// <summary>
        /// Gets or sets the current framerate.
        /// </summary>
        public double CurrentFramerate
        {
            get { return (double)GetValue(CurrentFramerateProperty); }
            set { SetValue(CurrentFramerateProperty, value); }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the FrameRateGraphControl
        /// </summary>
        public FramerateGraphControl()
        {
            // Required to initialize variables
            DefaultStyleKey = typeof (FramerateGraphControl);

            _updateTimer = new DispatcherTimer();
            _updateTimer.Tick += UpdateTimer_Tick;
        }

        public bool IsRecording
        {
            get { return _updateTimer.IsEnabled; }
        }

        public void StartRecording()
        {
            _updateTimer.Interval = ChartUpdateFrequency;
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _updateTimer.Start();
            }
        }

        public void StopRecording()
        {
            _updateTimer.Stop();
        }

        public void Reset()
        {
            lock (_threadSync)
            {
                m_currentLine = null;
                if (_frameRateGraphCanvas != null)
                {
                    var lines = _frameRateGraphCanvas.Children
                                                     .Where(i => i is Line)
                                                     .ToList();
                    lines.ForEach(i => _frameRateGraphCanvas.Children.Remove(i));
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            lock (_threadSync)
            {
                UpdateGraphValue(CurrentFramerate);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _frameRateLabel = GetTemplateChild(FrameRateLabelName) as TextBlock;
            _frameRateGraphCanvas = GetTemplateChild(FrameRateGraphCanvasName) as Canvas;
        }

        private void UpdateGraphValue(double fps)
        {
            Repaint(fps);
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            return kszDesiredSize;
        }

        /// <summary>
        /// Computes the Y value on the graph for a given fps value.
        /// </summary>
        /// <param name="fps">fps value to compute Y graph value for</param>
        /// <returns>The Y value on the graph for this fps value</returns>
        private double ComputeFpsGraphValue(double fps)
        {
            if (fps > knMaxFps)
            {
                fps = knMaxFps;
            }

            if (fps < 0.0)
            {
                fps = 0.0;
            }

            return knGraphLineMinY - (fps / knMaxFps) * (knGraphLineMinY - knGraphLineMaxY);
        }

        /// <summary>
        /// Creates a new line for the graph and initializes it's common properties
        /// </summary>
        /// <returns></returns>
        private Line CreateGraphLine()
        {
            Line line = new Line();
            line.Stroke = kbrGraphLineBrush;
            line.StrokeThickness = knGraphLineThickness;
            return line;
        }

        /// <summary>
        /// Repaints this graph based on the current frames per second.
        /// </summary>
        /// <param name="fps"></param>
        private void Repaint(double fps)
        {
            // Convert the fps to the nearest whole number
            fps = Math.Round(fps);

            // Update the frame rate text label
            _frameRateLabel.Text = fps.ToString();

            // Do we have a current graph line yet? If not,
            // then we haven't started displaying anything, 
            // so just create the first line and get out
            if (m_currentLine == null)
            {
                m_lastFps = fps;
                m_currentLine = CreateGraphLine();
                m_currentLine.X1 = m_currentLine.X2 = knGraphLineMinX;
                m_currentLine.Y1 = m_currentLine.Y2 = ComputeFpsGraphValue(fps);

                // Add the line to our canvas
                _frameRateGraphCanvas.Children.Add(m_currentLine);
                return;
            }

            // Has our frame rate value changed?
            if (m_lastFps != fps)
            {
                // Our frame rate value has changed, so we need to draw a 
                // new vertical line connecting the old value to the new value,
                // and then draw a new horizontal line representing the new value.
                // First, the horizontal line.
                Line horizontalLine = CreateGraphLine();

                // The horizontal line begins where the current line left off 
                horizontalLine.X1 = horizontalLine.X2 = m_currentLine.X2;
                horizontalLine.Y1 = horizontalLine.Y2 = ComputeFpsGraphValue(fps);

                // Add the line to our canvas
                _frameRateGraphCanvas.Children.Add(horizontalLine);

                // Now create the new vertical line
                Line verticalLine = CreateGraphLine();
                verticalLine.X1 = verticalLine.X2 = m_currentLine.X2;
                verticalLine.Y1 = m_currentLine.Y1;
                verticalLine.Y2 = horizontalLine.Y1;

                // Add this line to our canvas
                _frameRateGraphCanvas.Children.Add(verticalLine);

                // The current line becomes the horizontal line
                m_currentLine = horizontalLine;
            }
            else
            {
                // Our frame rate has not changed from our last repaint,
                // so increment the X value of our current line.
                if (m_currentLine.X2 < knGraphLineMaxX)
                {
                    m_currentLine.X2++;
                }
                else
                {
                    // Our current line has gone over our maximum x value,
                    // so let's walk over all the lines and collapse any that have fallen off the
                    // minimum X value of our graph
                    foreach (FrameworkElement fe in _frameRateGraphCanvas.Children)
                    {
                        Line graphLine = fe as Line;
                        if ((graphLine != null) && (graphLine.Visibility == Visibility.Visible))
                        {
                            if (graphLine.X1 > knGraphLineMinX)
                            {
                                graphLine.X1--;
                            }
                            if (graphLine.X2 > knGraphLineMinX)
                            {
                                graphLine.X2--;
                            }
                            else
                            {
                                graphLine.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    m_currentLine.X2++;
                }
            }

            // Now we have to purge our graph of any non-visible lines
            for (int i = 0; i < _frameRateGraphCanvas.Children.Count; i++)
            {
                Line graphLine = _frameRateGraphCanvas.Children[i] as Line;
                if ((graphLine != null) && (graphLine.Visibility == Visibility.Collapsed))
                {
                    _frameRateGraphCanvas.Children.Remove(graphLine);
                }
            }

            // Remember our fps for the next go around
            m_lastFps = fps;
        }
    }
}
