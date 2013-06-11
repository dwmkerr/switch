using EnvDTE;
using EnvDTE80;

namespace SwitchCore.SwitchTargets
{
    /// <summary>
    /// ISwitchTarget is an interface for an object that can switch the active document.
    /// </summary>
    public interface ISwitchTarget
    {
        /// <summary>
        /// Determines whether this instance can switch given the specified document.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns>
        ///   <c>true</c> if this instance can switch the specified application; otherwise, <c>false</c>.
        /// </returns>
        bool CanSwitch(DTE2 application, Document activeDocument);

        /// <summary>
        /// Switches the specified document.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns>True if switched successfully.</returns>
        bool DoSwitch(DTE2 application, Document activeDocument);
    }
}
