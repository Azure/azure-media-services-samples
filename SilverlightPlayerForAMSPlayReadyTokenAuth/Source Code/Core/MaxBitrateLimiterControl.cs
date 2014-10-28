using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Control used the limit the bitrate of a media.
    /// </summary>
    [TemplatePart(Name = LimitMaxBitrateSliderName, Type = typeof(Slider))]
    [TemplatePart(Name = NowDownloadingSliderName, Type = typeof(Slider))]
    [TemplatePart(Name = NowDownloadingBitrateLabelName, Type = typeof(TextBlock))]
    [TemplatePart(Name = MaxBitrateLimiterLabelName, Type = typeof(TextBlock))]
    public class MaxBitrateLimiterControl : Control
    {
        private const string LimitMaxBitrateSliderName = "LimitMaxBitrateSlider";
        private const string NowDownloadingSliderName = "NowDownloadingSlider";
        private const string NowDownloadingBitrateLabelName = "NowDownloadingBitrateLabel";
        private const string MaxBitrateLimiterLabelName = "MaxBitrateLimiterLabel";

        /// <summary>
        /// Occurs when this control recommends changing the maximum bitrate.
        /// </summary>
        public event EventHandler<CustomEventArgs<long>> RecommendMaximumBitrate;

        private Slider _limitMaxBitrateSlider;
        private Slider _nowDownloadingSlider;
        private TextBlock _nowDownloadingBitrateLabel;
        private TextBlock _maxBitrateLimiterLabel;

        #region AvailableBitrates
        /// <summary>
        /// AvailableBitrates DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty AvailableBitratesProperty =
            DependencyProperty.Register("AvailableBitrates", typeof(IEnumerable<long>), typeof(MaxBitrateLimiterControl), new PropertyMetadata(Enumerable.Empty<long>(), OnAvailableBitratesPropertyChanged));

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
            var maxBitrateLimiterControl = d as MaxBitrateLimiterControl;
            maxBitrateLimiterControl.IfNotNull(i => i.OnAvailableBitratesChanged());
        }
        #endregion

        #region DownloadBitrate
        /// <summary>
        /// DownloadBitrate DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty DownloadBitrateProperty =
            DependencyProperty.Register("DownloadBitrate", typeof(long), typeof(MaxBitrateLimiterControl), new PropertyMetadata((long)0, OnDownloadBitratePropertyChanged));

        /// <summary>
        /// Gets or sets the bitrate of the media currently being downloaded.
        /// </summary>
        public long DownloadBitrate
        {
            get { return (long)GetValue(DownloadBitrateProperty); }
            set { SetValue(DownloadBitrateProperty, value); }
        }

        private static void OnDownloadBitratePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var maxBitrateLimiterControl = d as MaxBitrateLimiterControl;
            maxBitrateLimiterControl.IfNotNull(i => i.OnDownloadBitrateChanged());
        }
        #endregion

        public MaxBitrateLimiterControl()
        {
            DefaultStyleKey = typeof (MaxBitrateLimiterControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _nowDownloadingBitrateLabel = GetTemplateChild(NowDownloadingBitrateLabelName) as TextBlock;
            _nowDownloadingSlider = GetTemplateChild(NowDownloadingSliderName) as Slider;
            _maxBitrateLimiterLabel = GetTemplateChild(MaxBitrateLimiterLabelName) as TextBlock;
            _limitMaxBitrateSlider = GetTemplateChild(LimitMaxBitrateSliderName) as Slider;
            _limitMaxBitrateSlider.IfNotNull(i => i.ValueChanged += LimitMaxBitrateSlider_ValueChanged);
        }

        private void OnDownloadBitrateChanged()
        {
            var bitrateDisplayValue = DownloadBitrate/1000;

            _nowDownloadingSlider.IfNotNull(i => i.Value = bitrateDisplayValue);
            _nowDownloadingBitrateLabel.IfNotNull(i => i.Text = bitrateDisplayValue.ToString());
        }

        private void OnAvailableBitratesChanged()
        {
            var minBitrate = AvailableBitrates.Any()
                                 ? AvailableBitrates.Min()
                                 : 0;

            var maxBitrate = AvailableBitrates.Any()
                                 ? AvailableBitrates.Max()
                                 : 0;

            if (_nowDownloadingSlider != null)
            {
                _nowDownloadingSlider.Minimum = 0;
                _nowDownloadingSlider.Maximum = maxBitrate / 1000.0;
                _nowDownloadingSlider.Value = 0;
            }

            if (_limitMaxBitrateSlider != null)
            {
                // Update the bitrate limiter slider
                _limitMaxBitrateSlider.Minimum = Math.Ceiling(minBitrate / 1000.0); // Round up otherwise we exclude bottom bitrate from eligibility
                _limitMaxBitrateSlider.Maximum = Math.Ceiling(maxBitrate / 1000.0); // Round up otherwise we exclude top bitrate from eligibility
                _limitMaxBitrateSlider.Value = _limitMaxBitrateSlider.Maximum;
            }
        }

        private void LimitMaxBitrateSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_maxBitrateLimiterLabel != null)
            {
                var maxBitrate = (ulong)(Math.Round(e.NewValue));
                _maxBitrateLimiterLabel.Text = maxBitrate.ToString();
            }
            
            RecommendMaximumBitrate.IfNotNull(i => i(this, new CustomEventArgs<long>((long)(e.NewValue * 1000))));
        }

    }
}
