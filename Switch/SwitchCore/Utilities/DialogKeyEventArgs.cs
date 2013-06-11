using System.Windows;
using System.Windows.Input;

namespace SwitchCore.Utilities
{
    /// <summary>
    /// Event args used by <see cref="UIElementDialogPage.DialogKeyPendingEvent"/>.
    /// </summary>
    public class DialogKeyEventArgs : RoutedEventArgs
    {
        internal DialogKeyEventArgs(RoutedEvent evt, Key key) : base(evt)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the key being pressed within the UIElementDialogPage.
        /// </summary>
        public Key Key
        {
            get;
            private set;
        }
    }
}