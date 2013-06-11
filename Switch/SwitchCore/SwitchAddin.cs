using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using SwitchCore.Configuration;
using SwitchCore.SwitchTargets;

namespace SwitchCore
{
    /// <summary>
    /// The Switch Addin singleton.
    /// </summary>
    public sealed class SwitchAddin
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="SwitchAddin"/> class from being created.
        /// </summary>
        private SwitchAddin()
        {
            
        }

        /// <summary>
        /// Initialises the host.
        /// </summary>
        /// <param name="applicationObject">The application object.</param>
        /// <param name="addInInstance">The add in instance.</param>
        public void InitialiseHost(DTE2 applicationObject, AddIn addInInstance)
        {
            this.applicationObject = applicationObject;
            this.addInInstance = addInInstance;
            LoadConfiguration();
        }
        /// <summary>
        /// Sets up the user interface.
        /// </summary>
        public void CreateCommands()
        {
            try
            {
                var contextUIGuids = new object[] { };

                //  Create the 'See IL' command.
                applicationObject.Commands.AddNamedCommand(addInInstance, Command_Switch_Name,
                    Command_Switch_Caption, Command_Switch_Tooltip, false, 1,
                   ref contextUIGuids, (int)vsCommandStatus.vsCommandStatusSupported);
            }
            catch (Exception)
            {
                //  Creating the command should only fail if we have it already.
            }
        }

        /// <summary>
        /// Creates the user interface.
        /// </summary>
        public void CreateUserInterface()
        {
            //  Get the sil command name.
            var silCommandId = addInInstance.ProgID + "." + Command_Switch_Name;

            //  If we don't have the command, create it.
            if (applicationObject.Commands.OfType<Command>().Any(c => c.Name != silCommandId))
                CreateCommands();

            //  Create the control for the Sil command.
            try
            {
                var commandSwitch = applicationObject.Commands.Item(silCommandId);

                //  Retrieve the context menu of code windows.
                var commandBars = (CommandBars)applicationObject.CommandBars;
                var mainMenuCommandBar = commandBars[MenuBar_MainMenu];

                //  Add the switch line command.
                controlMainMenuSwitchCommand = (CommandBarControl)commandSwitch.AddControl(mainMenuCommandBar,
                    mainMenuCommandBar.Controls.Count + 1);
            }
            catch (Exception exception)
            {
                HandleException(@"Failed to create the 'Switch' command.", exception);
            }
        }

        /// <summary>
        /// Destroys the user interface.
        /// </summary>
        public void DestroyUserInterface()
        {
            if (controlMainMenuSwitchCommand == null)
                return;

            try
            {
                controlMainMenuSwitchCommand.Delete();
            }
            catch
            {
                //  The application is shutting down, don't interfere with the user.
            }
        }

        /// <summary>
        /// Handles an exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        private static void HandleException(string message, Exception exception)
        {
            MessageBox.Show(message, "Error");
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="executeOption">The execute option.</param>
        /// <returns></returns>
        public bool ExecuteCommand(string commandName, vsCommandExecOption executeOption)
        {
            if (commandName == GetQualifiedCommandName(Command_Switch_Name))
            {
                ExecuteSwitchCommand();
                return true;
            }

            return false;
        }

        private void ExecuteSwitchCommand()
        {
            //  If we have no application or no document, we're done here.
            if (applicationObject == null || applicationObject.ActiveDocument == null)
                return;
            foreach (var target in BuildSwitchTargets())
                if (target.DoSwitch(applicationObject, applicationObject.ActiveDocument))
                    break;
        }

        /// <summary>
        /// Gets the name of the qualified command.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns></returns>
        private string GetQualifiedCommandName(string commandName)
        {
            return addInInstance.ProgID + "." + commandName;
        }

        /// <summary>
        /// Queries the status.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="neededText">The needed text.</param>
        /// <param name="status">The status.</param>
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status)
        {
            if (commandName == GetQualifiedCommandName(Command_Switch_Name) &&
                neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                status = (vsCommandStatus) vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
            }
        }

        /// <summary>
        /// Gets the switch configuration path.
        /// </summary>
        /// <returns></returns>
        private static string GetSwitchConfigurationPath()
        {
            return Path.Combine(
                Path.GetDirectoryName(typeof (SwitchConfiguration).Assembly.Location),
                @"SwitchConfiguration.xml");

        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public void LoadConfiguration()
        {
            if (!File.Exists(GetSwitchConfigurationPath()))
            {
                CreateDefaultConfiguration();
                return;
            }

            //  Open the file.
            try
            {
                using (var stream = new FileStream(GetSwitchConfigurationPath(), FileMode.Open, FileAccess.Read))
                {
                    var serailzier = new XmlSerializer(typeof (SwitchConfiguration));
                    Configuration = (SwitchConfiguration) serailzier.Deserialize(stream);
                }
            }
            catch (Exception exception)
            {
                HandleException(@"Failed to load the configuration for the Switch addin.", exception);
            }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public void SaveConfiguration()
        {
            //  Create the file.
            try
            {
                using (var stream = new FileStream(GetSwitchConfigurationPath(), FileMode.Create, FileAccess.Write))
                {
                    var serailzier = new XmlSerializer(typeof(SwitchConfiguration));
                    serailzier.Serialize(stream, Configuration);
                }
            }
            catch (Exception exception)
            {
                HandleException(@"Failed to load the configuration for the Switch addin.", exception);
            }
        }

        /// <summary>
        /// Creates the default configuration.
        /// </summary>
        public void CreateDefaultConfiguration()
        {
            Configuration = new SwitchConfiguration
                {
                    EnableSwitchBetweenDesignerAndCodeBehind = true,
                    EnableSwitchBetweenInterfaceAndImplementation = true,
                    ExtensionSwitches = new List<ExtensionSwitch>()
                };
            
            //  C/C++ extensions.
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("c", "h"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("cpp", "h"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("cxx", "h"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "c"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "cpp"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "cxx"));

            //  Add the XAML extensions.
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("xaml", "xaml.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("xaml.cs", "xaml"));

            //  Add the ASPX extensions.
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("master", "master.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("master.cs", "master.designer.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("master.designer.cs", "master"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("asax", "asax.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("asax.cs", "asax"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx", "aspx.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx.cs", "aspx.designer.cs"));
            Configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx.designer.cs", "aspx"));
        }

        /// <summary>
        /// Builds the switch targets.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ISwitchTarget> BuildSwitchTargets()
        {
            if(Configuration.EnableSwitchBetweenDesignerAndCodeBehind)
                yield return new DesignViewSwitchTarget();
            if(Configuration.EnableSwitchBetweenInterfaceAndImplementation)
                yield return new InterfaceSwitchTarget();
            foreach(var extensionSwitch in Configuration.ExtensionSwitches)
                yield return new ExtensionSwitchTarget(extensionSwitch.From, extensionSwitch.To);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SwitchAddin Instance
        {
            get { return instance; }
        }


        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static readonly SwitchAddin instance = new SwitchAddin();

        /// <summary>
        /// The application object.
        /// </summary>
        private DTE2 applicationObject;

        /// <summary>
        /// The add in instance.
        /// </summary>
        private AddIn addInInstance;

        /// <summary>
        /// The switch command control on the main menu.
        /// </summary>
        private CommandBarControl controlMainMenuSwitchCommand;

        //  Constants for the command names.
        private const string Command_Switch_Name = @"Switch";
        private const string Command_Switch_Caption = @"Switch";
        private const string Command_Switch_Tooltip = @"Switch between related files";

        //  Visual studio constants.
        private const string MenuBar_MainMenu = @"Menu Bar";

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public SwitchConfiguration Configuration { get; private set; }
        
    }
}
