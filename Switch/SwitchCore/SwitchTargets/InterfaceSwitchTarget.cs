using System.IO;
using EnvDTE;
using EnvDTE80;

namespace SwitchCore.SwitchTargets
{
    /// <summary>
    /// An Interface DoSwitch Target switches between an interface an its implementation.
    /// </summary>
    public class InterfaceSwitchTarget : ISwitchTarget
    {
        /// <summary>
        /// Switches the specified application.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns></returns>
        public bool DoSwitch(DTE2 application, Document activeDocument)
        {
            //  Do we have a file with or without an 'I'?
            var basePath = activeDocument.FullName;
            var implPath = Path.Combine(Path.GetDirectoryName(basePath), Path.GetFileName(basePath).Substring(1));
            var intPath = Path.Combine(Path.GetDirectoryName(basePath), "I" + Path.GetFileName(basePath));

            if (File.Exists(implPath))
                return SwitchHelper.TryOpenDocument(application, implPath);

            if (File.Exists(intPath))
                return SwitchHelper.TryOpenDocument(application, intPath);

            return false;
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
    }
}
