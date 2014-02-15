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
                SwitchCommand.Execute(applicationObject, Configuration);
                return true;
            }

            return false;
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
        /// Resets the addin to the default configuration.
        /// </summary>
        public void LoadDefaultConfiguration()
        {
            Configuration = SwitchConfigurationManager.CreateDefaultConfiguration();
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                Configuration = SwitchConfigurationManager.LoadConfiguration();
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
                SwitchConfigurationManager.SaveConfiguration(Configuration);
            }
            catch (Exception exception)
            {
                HandleException(@"Failed to load the configuration for the Switch addin.", exception);
            }
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
