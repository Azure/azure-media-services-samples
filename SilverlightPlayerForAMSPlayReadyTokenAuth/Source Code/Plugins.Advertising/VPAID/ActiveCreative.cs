namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VPAID
{
    /// <summary>
    /// An actively running creative.
    /// </summary>
    public class ActiveCreative
    {
        public ActiveCreative(IVpaid Player, ICreativeSource Source, IAdTarget Target)
        {
            this.Player = Player;
            this.Source = Source;
            this.Target = Target;
        }

        /// <summary>
        /// The VPaid handler responsible for playing the creative.
        /// </summary>
        public IVpaid Player { get; private set; }

        /// <summary>
        /// The source for the creative.
        /// </summary>
        public ICreativeSource Source { get; private set; }

        /// <summary>
        /// The target for the creative
        /// </summary>
        public IAdTarget Target { get; private set; }

        /// <summary>
        /// Helps us know if the user is coming out of mute when the volume changes
        /// </summary>
        internal double PreviousVolume { get; set; }
    }
}
