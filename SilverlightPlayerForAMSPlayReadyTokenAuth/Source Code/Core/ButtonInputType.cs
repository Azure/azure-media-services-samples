namespace Microsoft.SilverlightMediaFramework.Core
{
    /// <summary>
    /// Determines how to interpret when the user clicks and holds the mouse button down on the Fast Forward or Rewind buttons.
    /// </summary>
    /// <remarks>
    /// This enumeration determines whether a user holding the mouse button down on the Fast Forward or Rewind buttons is interpreted as a request to continue 
    /// that action while the mouse button is being held down or as just one mouse click.
    /// </remarks>
    public enum ButtonInputType
    {
        ButtonHold = 0,
        ClickToggle
    }
}