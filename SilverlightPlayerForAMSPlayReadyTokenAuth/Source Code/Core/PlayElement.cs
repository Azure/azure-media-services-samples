using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
using Microsoft.SilverlightMediaFramework.Utilities.Extensions;

namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Represents a control that offers the user a button to play (if paused) or pause (if playing) playback of the current media item.
    /// </summary>
    public partial class PlayElement : Button
    {
        #region PlayState
        /// <summary>
        /// PlayState DependencyProperty definition.
        /// </summary>
        public static readonly DependencyProperty PlayStateProperty =
            DependencyProperty.Register("PlayState", typeof (MediaPluginState), typeof (PlayElement),
                                        new PropertyMetadata(MediaPluginState.Stopped, OnPlayStatePropertyChanged));

        /// <summary>
        /// Gets the current media element state.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public MediaPluginState PlayState
        {
            get { return (MediaPluginState) GetValue(PlayStateProperty); }
            set { SetValue(PlayStateProperty, value); }
        }

        private static void OnPlayStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var player = d as PlayElement;
            player.IfNotNull(i => i.OnPlayStateChanged());
        }

        #endregion

        public PlayElement()
        {
            DefaultStyleKey = typeof (PlayElement);
        }

        private void OnPlayStateChanged()
        {
            this.GoToVisualState(PlayState.ToString());
        }
    }
}