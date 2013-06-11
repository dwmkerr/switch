using EnvDTE;
using EnvDTE80;

namespace SwitchCore.SwitchTargets
{
    /// <summary>
    /// An Extension DoSwitch Target switches files based on extension.
    /// </summary>
    public class ExtensionSwitchTarget : ISwitchTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionSwitchTarget"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public ExtensionSwitchTarget(string from, string to)
        {
            From = from;
            To = to;
        }
        
        /// <summary>
        /// Switches the specified document.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns>
        /// True if switched successfully.
        /// </returns>
        public bool DoSwitch(DTE2 application, Document activeDocument)
        {
            //  Get the doc path.
            var path = activeDocument.FullName;

            //  If there's no from path then we can't continue.
            if (path.EndsWith(From) == false)
                return false;

            //  Replace the path 'from' with the part 'to'.
            if (path.Length < From.Length)
                return false;

            //  Map the path.
            var mappedPath = path.Substring(0, path.Length - From.Length) + To;

            //  Try and open the mapped path.
            return SwitchHelper.TryOpenDocument(application, mappedPath);
        }

        /// <summary>
        /// Determines whether this instance can switch given the specified document.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns>
        ///   <c>true</c> if this instance can switch the specified application; otherwise, <c>false</c>.
        /// </returns>
        public bool CanSwitch(DTE2 application, Document activeDocument)
        {
            //  For now we always enable the command.
            return true;
        }

        /// <summary>
        /// Gets from.
        /// </summary>
        public string From
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets to.
        /// </summary>
        public string To
        {
            get;
            private set;
        }
    }
}
