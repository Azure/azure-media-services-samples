using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.SilverlightMediaFramework.Core.TemplateDefinitions;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents the Volume control on the Player.
    /// </summary>
    public partial class VolumeControl : Control
    {
        /// <summary>
        /// VolumeProperty DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("VolumeLevel", typeof (double), typeof (VolumeControl),
                                        new PropertyMetadata((double)0, OnVolumeLevelChanged));

        /// <summary>
        /// IsMuted DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsMutedProperty =
            DependencyProperty.Register("IsMuted", typeof (bool), typeof (VolumeControl),
                                        new PropertyMetadata(new PropertyChangedCallback(OnIsMutedChanged)));

        /// <summary>
        /// IsExpanded DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof (bool), typeof (VolumeControl),
                                        new PropertyMetadata(new PropertyChangedCallback(OnIsExpandedChanged)));

        private bool _bAnimateVolume = true;
        private bool _bExpandOnMouseOver = true;
        private FrameworkElement _baseElement;
        private FrameworkElement _expandingElement;
        private double _expandingElementFullHeight = 100;
        private bool _isAnimating;
        private Slider _slider;

        public VolumeControl()
        {
            DefaultStyleKey = typeof (VolumeControl);

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                IsEnabledChanged += VolumeControl_IsEnabledChanged;
#if !WINDOWS_PHONE
                Application.Current.Host.Content.FullScreenChanged += OnFullScreenChanged;
#endif
            }
        }

        /// <summary>
        /// Gets or sets whether to expand the VolumeControl on mouseover.
        /// </summary>
        public bool ExpandOnMouseOver
        {
            get { return _bExpandOnMouseOver; }
            set { _bExpandOnMouseOver = value; }
        }

        /// <summary>
        /// Gets or sets whether to animate the VolumeControl when the volume level is changed.
        /// </summary>
        public bool AnimateVolume
        {
            get { return _bAnimateVolume; }
            set { _bAnimateVolume = value; }
        }

        /// <summary>
        /// Gets or sets whether the media is muted.
        /// </summary>
        public bool IsMuted
        {
            get { return (bool) GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the VolumeControl is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool) GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        private double VolumeLevelBeforeMuted { get; set; }

        /// <summary>
        /// Gets or sets the volume level.
        /// </summary>
        public double VolumeLevel
        {
            get { return (double) GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        // ExpandingElementFullHeight is used to determine if transition to expanded state has completed

        
        private double ExpandingElementFullHeight
        {
            get { return _expandingElementFullHeight; }
            set { _expandingElementFullHeight = value; }
        }

        /// <summary>
        /// Occurs when the volume level changes.
        /// </summary>
        public event EventHandler<CustomEventArgs<double>> VolumeLevelChanged;
        
        /// <summary>
        /// Occurs when the volume is muted.
        /// </summary>
        public event EventHandler Muted;

        /// <summary>
        /// Occurs when the volume is unmuted.
        /// </summary>
        public event EventHandler UnMuted;
#if !WINDOWS_PHONE
        private void OnFullScreenChanged(object sender, EventArgs e)
        {
            if (!Application.Current.Host.Content.IsFullScreen)
            {
                IsExpanded = false;
            }
        }
#endif
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            IsExpanded = false;

            UninitializeTemplateChildren();
            GetTemplateChildren();
            InitializeTemplateChildren();


            if (VolumeLevel == 0)
            {
                IsMuted = true;
                VolumeLevelBeforeMuted = .5;
            }

            VisualStateManager.GoToState(this,
                                         IsMuted
                                             ? VolumeControlVisualStates.MutedStates.Muted
                                             : VolumeControlVisualStates.MutedStates.VolumeOn, true);
        }

        private void GetTemplateChildren()
        {
            _slider = GetTemplateChild(VolumeControlTemplateParts.SliderElement) as Slider;
            _baseElement = GetTemplateChild(VolumeControlTemplateParts.BaseElement) as FrameworkElement;
            _expandingElement = GetTemplateChild(VolumeControlTemplateParts.ExpandingElement) as FrameworkElement;
        }

        private void InitializeTemplateChildren()
        {
            if (_slider != null)
            {
                var sliderBinding = new Binding("VolumeLevel")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };

                _slider.SetBinding(RangeBase.ValueProperty, sliderBinding);
            }

            if (_baseElement != null)
            {
                _baseElement.MouseLeftButtonDown += BaseElement_MouseLeftButtonDown;
                _baseElement.MouseLeftButtonUp += BaseElement_MouseLeftButtonUp;
            }
        }

        private void UninitializeTemplateChildren()
        {
            if (_baseElement != null)
            {
                _baseElement.MouseLeftButtonDown -= BaseElement_MouseLeftButtonDown;
                _baseElement.MouseLeftButtonUp -= BaseElement_MouseLeftButtonUp;
            }
        }


        // Template Control Events

        private void VolumeControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vc = sender as VolumeControl;

            if (!IsEnabled && IsExpanded)
                IsExpanded = false;

            VisualStateManager.GoToState(vc,
                                         ((bool) e.NewValue)
                                             ? VolumeControlVisualStates.CommonStates.Normal
                                             : VolumeControlVisualStates.CommonStates.Disabled, true);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            VisualStateManager.GoToState(this, VolumeControlVisualStates.CommonStates.MouseOver, true);

            if (ExpandOnMouseOver && IsEnabled)
                IsExpanded = true;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            VisualStateManager.GoToState(this, VolumeControlVisualStates.CommonStates.Normal, true);

            if (ExpandOnMouseOver && IsEnabled)
            {
                // Default behavior when changing states is to stop the existing animation / transition.  So if 
                // we have a delay before closing, and the user mouses in and out real quick, the volume will partially
                // open and be stuck in that state because of the delay before closing.  This code will force the expand
                // animation to complete (.1 seconds), then trigger the close
                if (_expandingElement != null && _expandingElement.Height < ExpandingElementFullHeight)
                {
                    DoubleAnimationHelper(
                        _expandingElement.Height,
                        ExpandingElementFullHeight,
                        .15,
                        null,
                        ((s, a) => IsExpanded = false),
                        _expandingElement,
                        "Height");
                }
                else
                {
                    IsExpanded = false;
                }
            }
        }

        private void BaseElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, VolumeControlVisualStates.CommonStates.Pressed, true);

            if (IsMuted)
            {
                // Going from Muted to UnMuted
                IsMuted = false;
                UnMuted.IfNotNull(i => i(this, EventArgs.Empty));
            }
            else
            {
                // Going from UnMuted to Muted
                IsMuted = true;
                Muted.IfNotNull(i => i(this, EventArgs.Empty));
            }
            e.Handled = true;
        }

        private void BaseElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, VolumeControlVisualStates.CommonStates.MouseOver, true);
            e.Handled = true;
        }

        // Dependency Property Events

        private static void OnVolumeLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vc = d as VolumeControl;
            Debug.Assert(vc != null, "VolumeControl is null");
            if (vc._slider != null)
            {
                if (!vc._isAnimating)
                {
                    if (vc.IsMuted)
                    {
                        // User is interacting w/ slider while Muted.  Turn off IsMuted and set
                        // VolumeLevelBeforeMuted to -1 so OnIsMutedChanged doesn't animate to pre-Mute Volume
                        vc.VolumeLevelBeforeMuted = -1;
                        vc.IsMuted = false;
                        vc.UnMuted.IfNotNull(i => i(vc, EventArgs.Empty));
                    }
                    else if (vc.VolumeLevel == 0)
                    {
                        // user scrubbed to 0.  Need to mock a Muted state.  Set the pre mute level to .5 so they
                        // have somewhere to go if they click "unmute"
                        vc.IsMuted = true;
                        vc.VolumeLevelBeforeMuted = .5;
                    }
                }

                vc.VolumeLevelChanged.IfNotNull(i => i(vc, new CustomEventArgs<double>((double)e.NewValue)));
            }
            else
            {
                if (!vc.IsMuted && vc.VolumeLevel == 0)
                {
                    vc.IsMuted = true;
                    vc.VolumeLevelBeforeMuted = .5;
                }
            }
        }

        private static void OnIsMutedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vc = d as VolumeControl;
            Debug.Assert(vc != null, "VolumeControl is null");

            if ((bool) e.NewValue)
            {
                // preserve current volume
                vc.VolumeLevelBeforeMuted = vc.VolumeLevel;

                // Set VolumeLevel to 0 for Muting
                if (vc.AnimateVolume)
                    vc.DoubleAnimationHelper(vc.VolumeLevel, 0, .1, null, null, vc, "VolumeLevel");
                else
                    vc.VolumeLevel = 0;
            }
            else
            {
                if (vc.VolumeLevelBeforeMuted > 0)
                {
                    // restore previous volume
                    if (vc.AnimateVolume)
                        vc.DoubleAnimationHelper(vc.VolumeLevel, vc.VolumeLevelBeforeMuted, .1, null, null, vc,
                                                 "VolumeLevel");
                    else
                        vc.VolumeLevel = vc.VolumeLevelBeforeMuted;
                }
            }


            VisualStateManager.GoToState(vc,
                                         ((bool) e.NewValue)
                                             ? VolumeControlVisualStates.MutedStates.Muted
                                             : VolumeControlVisualStates.MutedStates.VolumeOn, true);
        }

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vc = d as VolumeControl;
            Debug.Assert(vc != null, "VolumeControl is null");
            VisualStateManager.GoToState(vc,
                                         ((bool) e.NewValue)
                                             ? VolumeControlVisualStates.ExpandedStates.Expanded
                                             : VolumeControlVisualStates.ExpandedStates.Collapsed, true);
        }

        private void DoubleAnimationHelper(double from, double to, double? seconds, double? speedRatio,
                                           EventHandler onCompleted, DependencyObject obj, string property)
        {
            _isAnimating = true;
            var sb = new Storyboard();

            if (onCompleted != null)
                sb.Completed += onCompleted;

            var animation = new DoubleAnimation
            {
                From = from,
                To =  to
            };

            if (speedRatio == null)
            {
                animation.Duration = TimeSpan.FromSeconds(seconds == null ? 1 : (double) seconds);
            }
            else
            {
                animation.SpeedRatio = (double) speedRatio;
            }

            Storyboard.SetTarget(animation, obj);
            Storyboard.SetTargetProperty(animation,
                                         new PropertyPath(
                                             string.Format(CultureInfo.InvariantCulture, "({0})", property),
                                             new object[0]));
            sb.Children.Add(animation);
            sb.Completed += ((s, e) => _isAnimating = false);
            sb.Begin();
        }
    }
}