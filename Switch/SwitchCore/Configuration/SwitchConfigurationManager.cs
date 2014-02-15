using SwitchCore.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchCore.Configuration
{
    public class SwitchConfigurationManager
    {
        /// <summary>
        /// Gets the switch configuration path.
        /// </summary>
        /// <returns></returns>
        private static string GetSwitchConfigurationPath()
        {
            return Path.Combine(
                Path.GetDirectoryName(typeof(SwitchConfigurationManager).Assembly.Location),
                @"SwitchConfiguration.xml");

        }
        
        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public static SwitchConfiguration LoadConfiguration()
        {
            if (!File.Exists(GetSwitchConfigurationPath()))
            {
                return CreateDefaultConfiguration();
            }

            //  Open the file.
            try
            {
                using (var stream = new FileStream(GetSwitchConfigurationPath(), FileMode.Open, FileAccess.Read))
                {
                    var serailzier = new XmlSerializer(typeof(SwitchConfiguration));
                    return (SwitchConfiguration)serailzier.Deserialize(stream);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(@"Failed to load the configuration for the Switch addin.", exception);
            }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public static void SaveConfiguration(SwitchConfiguration switchConfiguration)
        {
            //  Create the file.
            try
            {
                using (var stream = new FileStream(GetSwitchConfigurationPath(), FileMode.Create, FileAccess.Write))
                {
                    var serailzier = new XmlSerializer(typeof(SwitchConfiguration));
                    serailzier.Serialize(stream, switchConfiguration);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(@"Failed to load the configuration for the Switch addin.", exception);
            }
        }

        /// <summary>
        /// Creates the default configuration.
        /// </summary>
        public static SwitchConfiguration CreateDefaultConfiguration()
        {
            var configuration = new SwitchConfiguration
            {
                EnableSwitchBetweenDesignerAndCodeBehind = true,
                EnableSwitchBetweenInterfaceAndImplementation = true,
                ExtensionSwitches = new List<ExtensionSwitch>()
            };

            //  C/C++ extensions.
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("c", "h"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("cpp", "h"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("cxx", "h"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "c"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "cpp"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("h", "cxx"));

            //  Add the XAML extensions.
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("xaml", "xaml.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("xaml.cs", "xaml"));

            //  Add the ASPX extensions.
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("master", "master.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("master.cs", "master.designer.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("master.designer.cs", "master"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("asax", "asax.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("asax.cs", "asax"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx", "aspx.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx.cs", "aspx.designer.cs"));
            configuration.ExtensionSwitches.Add(new ExtensionSwitch("aspx.designer.cs", "aspx"));

            //  Return the configuration.
            return configuration;
        }
    }
}
