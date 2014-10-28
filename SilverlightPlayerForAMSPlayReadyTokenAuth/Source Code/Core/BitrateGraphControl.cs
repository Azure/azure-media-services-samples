using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    [TemplatePart(Name = BitrateGraphCanvasName, Type = typeof(Canvas))]
    public class BitrateGraphControl : Control
    {
        private const string BitrateGraphCanvasName = "BitrateGraphCanvas";

        /// <summary>
        /// The height of each bitrate item (label, background, and tracker).
        /// </summary>
        private const short knBitrateGraphItemHeight = 25;

        /// <summary>
        /// The maximum X value of the bitrate graph lines.
        /// </summary>
        private const double knBitrateGraphLineMaxX = 500.0;

        /// <summary>
        /// The starting X value of the bitrate graph lines
        /// </summary>
        private const double knBitrateGraphLineMinX = 60.0;

        /// <summary>
        /// The offset of our graph lines from the label position to get them 
        /// centered.
        /// </summary>
        private const double knBitrateGraphLineOffsetFromLabel = 10.0;
        /// <summary>
        /// The left alignmnet of the bitrate labels on the graph
        /// </summary>
        private const double knBitrateLabelMinX = 5.0;

        /// <summary>
        /// The top alignment of the first bitrate label
        /// </summary>
        private const double knBitrateLabelMinY = 5.0;

        /// <summary>
        /// Extra space at the bottom to make the graph look better
        /// </summary>
        private const double knGraphBottomMargin = 10.0;

        /// <summary>
        /// Extra space on the right to make the graph look good
        /// </summary>
        private const double knGraphRightMargin = 20.0;

        /// <summary>
        /// The list of bitrates we are tracking
        /// </summary>
        private readonly List<ulong> m_bitrateList = new List<ulong>();

        private BitrateGraph m_currentBitrateGraph = new BitrateGraph(new SolidColorBrush(Colors.Red));

        private BitrateGraph m_highestPlayableGraph = new BitrateGraph(new SolidColorBrush(Colors.Green));

        /// <summary>
        /// The Y values for all of the graph lines.
        /// </summary>
        private readonly List<double> m_graphLineYValues = new List<double>();

        /// <summary>
        /// The list of all the lines in our graph
        /// </summary>
        private readonly List<Line> m_lines = new List<Line>();

        private Canvas _bitrateGraphCanvas;
        private DispatcherTimer _updateTimer;



        #region AvailableBitrates
        /// <summary>
        /// AvailableBitrates DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AvailableBitratesProperty =
            DependencyProperty.Register("AvailableBitrates", typeof(IEnumerable<long>), typeof(BitrateGraphControl), new PropertyMetadata(Enumerable.Empty<long>(), OnAvailableBitratesPropertyChanged));

        /// <summary>
        /// Gets or sets the bitrates that are currently available.
        /// </summary>
        public IEnumerable<long> AvailableBitrates
        {
            get { return (IEnumerable<long>)GetValue(AvailableBitratesProperty); }
            set { SetValue(AvailableBitratesProperty, value); }
        }

        private static void OnAvailableBitratesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bitrateGraphControl = d as BitrateGraphControl;
            bitrateGraphControl.IfNotNull(i => i.OnAvailableBitratesChanged());
        }

        private void OnAvailableBitratesChanged()
        {
            if (isLoaded)
            {
                var newBitrates = AvailableBitrates.Select(i => (ulong)i)
                                                   .ToArray();
                UpdateBitrateList(newBitrates);
            }
        }
        #endregion

        #region CurrentBitrate
        /// <summary>
        /// CurrentBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty CurrentBitrateProperty =
            DependencyProperty.Register("CurrentBitrate", typeof(long), typeof(BitrateGraphControl), new PropertyMetadata((long)0));

        /// <summary>
        /// Gets or sets the currently active bitrate.
        /// </summary>
        public long CurrentBitrate
        {
            get { return (long)GetValue(CurrentBitrateProperty); }
            set { SetValue(CurrentBitrateProperty, value); }
        }
        #endregion

        #region MaximumBitrate
        /// <summary>
        /// MaximumPlayableBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty MaximumPlayableBitrateProperty =
            DependencyProperty.Register("MaximumPlayableBitrate", typeof(long), typeof(BitrateGraphControl), new PropertyMetadata((long)0));

        /// <summary>
        /// Gets or sets the current maximum playable bitrate.
        /// </summary>
        public long MaximumPlayableBitrate
        {
            get { return (long)GetValue(MaximumPlayableBitrateProperty); }
            set { SetValue(MaximumPlayableBitrateProperty, value); }
        }
        #endregion

        #region ChartUpdateFrequency
        /// <summary>
        /// ChartUpdateFrequency DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ChartUpdateFrequencyProperty =
            DependencyProperty.Register("ChartUpdateFrequency", typeof(TimeSpan), typeof(BitrateGraphControl), new PropertyMetadata(TimeSpan.FromMilliseconds(70)));

        /// <summary>
        /// Gets or sets the frequency with which the charts are updated.
        /// </summary>
        public TimeSpan ChartUpdateFrequency
        {
            get { return (TimeSpan)GetValue(ChartUpdateFrequencyProperty); }
            set { SetValue(ChartUpdateFrequencyProperty, value); }
        }
        #endregion





        public BitrateGraphControl()
        {
            DefaultStyleKey = typeof (BitrateGraphControl);

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
            m_lines.Clear();
            _bitrateGraphCanvas.IfNotNull(i => i.Children.Clear());
            m_currentBitrateGraph = new BitrateGraph(new SolidColorBrush(Colors.Red));
            m_highestPlayableGraph = new BitrateGraph(new SolidColorBrush(Colors.Green));
        }

        bool isLoaded;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _bitrateGraphCanvas = GetTemplateChild(BitrateGraphCanvasName) as Canvas;

            isLoaded = true;

            if (AvailableBitrates.Any())
            {
                OnAvailableBitratesChanged();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateGraphValue((ulong) CurrentBitrate, (ulong)MaximumPlayableBitrate);
        }


        private void RepaintGraph(BitrateGraph graph, ulong bitrate)
        {
            // Do we have a current graph line yet? If not,
            // then we haven't started displaying anything, 
            // so just create the first line and get out
            if (graph.CurrentLine == null)
            {
                graph.LastBitrate = bitrate;
                graph.CurrentLine = CreateGraphLine(knBitrateGraphLineMinX, ComputeBitrateGraphValue(bitrate),
                    knBitrateGraphLineMinX, ComputeBitrateGraphValue(bitrate), graph.Color);

                // Add the line to our canvas
                _bitrateGraphCanvas.Children.Add(graph.CurrentLine);
                return;
            }

            // Has our frame rate value changed?
            if (graph.LastBitrate != bitrate)
            {
                // Our frame rate value has changed, so we need to draw a 
                // new vertical line connecting the old value to the new value,
                // and then draw a new horizontal line representing the new value.
                // First, the horizontal line.
                Line horizontalLine = CreateGraphLine(graph.CurrentLine.X2, ComputeBitrateGraphValue(bitrate),
                    graph.CurrentLine.X2, ComputeBitrateGraphValue(bitrate), graph.Color);

                // Add the line to our canvas
                _bitrateGraphCanvas.Children.Add(horizontalLine);

                // Now create the new vertical line
                Line verticalLine = CreateGraphLine(graph.CurrentLine.X2, graph.CurrentLine.Y1,
                    graph.CurrentLine.X2, horizontalLine.Y1, graph.Color);

                // Add this line to our canvas
                _bitrateGraphCanvas.Children.Add(verticalLine);

                // The current line becomes the horizontal line
                graph.CurrentLine = horizontalLine;
            }
            else
            {
                // Our frame rate has not changed from our last repaint,
                // so increment the X value of our current line.
                if (graph.CurrentLine.X2 < knBitrateGraphLineMaxX)
                {
                    graph.CurrentLine.X2++;
                }
                else
                {
                    // Our current line has gone over our maximum x value,
                    // so let's walk over all the lines and collapse any that have fallen off the
                    // minimum X value of our graph
                    foreach (Line graphLine in m_lines)
                    {
                        if ((graphLine != null) && (graphLine.Visibility == Visibility.Visible))
                        {
                            if (graphLine.X1 > knBitrateGraphLineMinX)
                            {
                                graphLine.X1--;
                            }
                            if (graphLine.X2 > knBitrateGraphLineMinX)
                            {
                                graphLine.X2--;
                            }
                            else
                            {
                                graphLine.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    graph.CurrentLine.X2++;
                }
            }

            // Remember our fps for the next go around
            graph.LastBitrate = bitrate;
        }


        private void Repaint(ulong bitrate, ulong highestPlayableBitrate)
        {
            //
            //  Don't paint the bitrate if it hasn't been set yet
            //
            if (bitrate == 0)
            {
                return;
            }

            RepaintGraph(m_currentBitrateGraph, bitrate);
            RepaintGraph(m_highestPlayableGraph, highestPlayableBitrate);

            // Now we have to purge our graph of any non-visible lines
            for (int i = m_lines.Count - 1; i >= 0; --i)
            {
                if (m_lines[i].Visibility == Visibility.Collapsed)
                {
                    Line line = m_lines[i];
                    m_lines.RemoveAt(i);
                }
                else
                {
                    // Make sure our tracker lines are always on top. This is hacky,
                    // and I am not sure why this control sometimes loses the z-order.
                    // It's late and I don't feel like tracking it down, so in the meantime,
                    // this works.
                    _bitrateGraphCanvas.Children.Remove(m_lines[i]);
                    _bitrateGraphCanvas.Children.Add(m_lines[i]);
                }
            }
        }

        private void UpdateBitrateList(IEnumerable<ulong> bitrates)
        {
            // Add the bitrates to our internal list
            m_bitrateList.Clear();
            foreach (ulong bitrate in bitrates)
            {
                m_bitrateList.Add(bitrate);
            }

            // Sort them in descending order.
            m_bitrateList.Sort(CompareDescending);

            // Add all of the background information for the graph. This
            // includes the bitrate text labels and background lines
            double currentLabelPosition = knBitrateLabelMinY;

            foreach (int bitrate in m_bitrateList)
            {
                // First create the bitrate text block
                TextBlock bitrateLabel = new TextBlock();

                // Set the forground color to white
                bitrateLabel.Foreground = new SolidColorBrush(Colors.White);

                // Truncate the label text if greater than 1 mbps
                if ((double)(bitrate / 1000.0) > 1000.0)
                {
                    bitrateLabel.Text = ((double)(bitrate / 1000.0) / 1000.0).ToString() + "M";
                }
                else
                {
                    bitrateLabel.Text = ((double)(bitrate / 1000.0)).ToString() + "K";
                }

                // Set the position of the label
                bitrateLabel.SetValue(Canvas.LeftProperty, knBitrateLabelMinX);
                bitrateLabel.SetValue(Canvas.TopProperty, currentLabelPosition);
                bitrateLabel.Tag = bitrate.ToString();

                // Add the label to the canvas
                _bitrateGraphCanvas.Children.Add(bitrateLabel);

                // Now create a gray background line for this item
                Line backgroundLine = new Line();
                backgroundLine.X1 = knBitrateGraphLineMinX;
                backgroundLine.X2 = knBitrateGraphLineMaxX;
                backgroundLine.Y1 = currentLabelPosition + knBitrateGraphLineOffsetFromLabel;
                backgroundLine.Y2 = currentLabelPosition + knBitrateGraphLineOffsetFromLabel;
                backgroundLine.Tag = "Gray";
                backgroundLine.Stroke = new SolidColorBrush(Colors.LightGray);
                backgroundLine.StrokeThickness = 1.5;
                m_graphLineYValues.Add(currentLabelPosition + knBitrateGraphLineOffsetFromLabel);

                // Add the line to the canvas
                _bitrateGraphCanvas.Children.Add(backgroundLine);

                // Increment the current top position
                currentLabelPosition += knBitrateGraphItemHeight;
            }
            InvalidateMeasure();
        }

        private void UpdateGraphValue(ulong bitrate, ulong highestPlayableBitrate)
        {
            Repaint(bitrate, highestPlayableBitrate);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(knBitrateGraphLineMaxX + knGraphRightMargin, knGraphBottomMargin + (m_bitrateList.Count) * knBitrateGraphItemHeight);
        }

        /// <summary>
        /// A simple comparer for use in a list sort.
        /// </summary>
        /// <param name="x">First object to compare</param>
        /// <param name="y">Second object to compare</param>
        /// <returns>positive if y greater than x, negative if y less than x, else 0</returns>
        static int CompareDescending(ulong x, ulong y)
        {
            return y.CompareTo(x);
        }

        private double ComputeBitrateGraphValue(ulong bitrate)
        {
            for (int i = 0; i < m_bitrateList.Count && i < m_graphLineYValues.Count; ++i)
            {
                if (bitrate == m_bitrateList[i])
                {
                    return m_graphLineYValues[i];
                }
            }
            return 0;
        }

        private Line CreateGraphLine(double X1, double Y1, double X2, double Y2, SolidColorBrush color)
        {
            Line line = new Line();
            line = new Line();
            line.X1 = X1;
            line.Y1 = Y1;
            line.X2 = X2;
            line.Y2 = Y2;
            line.Stroke = color;
            line.StrokeThickness = 2.5;
            line.Tag = "GraphLine";

            // Keep track of all the lines we create
            m_lines.Add(line);
            return line;
        }

        private class BitrateGraph
        {
            public BitrateGraph(SolidColorBrush color)
            {
                Color = color;
            }

            /// <summary>
            /// The current line we are drawing
            /// </summary>
            public Line CurrentLine;

            /// <summary>
            /// The last bitrate we drew.
            /// </summary>
            public ulong LastBitrate = 0;

            public SolidColorBrush Color;
        }
    }

    
}
