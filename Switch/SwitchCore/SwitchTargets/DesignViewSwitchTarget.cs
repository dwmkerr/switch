using System.IO;
using EnvDTE;
using EnvDTE80;

namespace SwitchCore.SwitchTargets
{
    /// <summary>
    /// The design view switch target tries to switch to and from design view.
    /// </summary>
    public class DesignViewSwitchTarget : ISwitchTarget
    {
        /// <summary>
        /// Determines whether this instance can attempt to switch given the specified path.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="activeDocument">The active document.</param>
        /// <returns>
        ///   <c>true</c> if this instance can attempt to switch given the specified path; otherwise, <c>false</c>.
        /// </returns>
        public bool DoSwitch(DTE2 application, Document activeDocument)
        {
            //  Get the path.
            var path = activeDocument.FullName;

            //  Create the designer path.
            string designerPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)) + ".designer.cs";

            //  If there's no designer, we can't switch.
            if (File.Exists(designerPath) == false)
                return false;

            //  If we're showing the designer, show the code view.
            if (activeDocument.ActiveWindow.Caption.Contains("[Design]"))
                activeDocument.ProjectItem.Open(VsViewKindCode).Activate();
            else
                activeDocument.ProjectItem.Open(VsViewKindDesigner).Activate();

            //  We can never know if it worked...
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

        internal const string VsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";
        internal const string VsViewKindDebugging = "{7651A700-06E5-11D1-8EBD-00A0C90F26EA}";
        internal const string VsViewKindDesigner = "{7651A702-06E5-11D1-8EBD-00A0C90F26EA}";
    }
}
