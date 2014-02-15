using Microsoft.VisualStudio.Shell;
using SwitchCore.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveKerr.Switch2013
{
    class SwitchOptionsPage : DialogPage, ISwitchConfiguration
    {
        public SwitchOptionsPage()
        {
            var defaultConfig = SwitchConfigurationManager.CreateDefaultConfiguration();
            enableSwitchBetweenDesignerAndCodeBehind = defaultConfig.EnableSwitchBetweenDesignerAndCodeBehind;
            enableSwitchBetweenDesignerAndCodeBehind = defaultConfig.EnableSwitchBetweenDesignerAndCodeBehind;
            foreach(var extension in defaultConfig.ExtensionSwitches)
                extensionSwitches.Add(extension);
        }

        private bool enableSwitchBetweenInterfaceAndImplementation = true;
        private bool enableSwitchBetweenDesignerAndCodeBehind = true;
        private List<ExtensionSwitch> extensionSwitches = new List<ExtensionSwitch>();
        [Category("Switching")]
        [DisplayName("Switch between Interface and Implmentation")]
        [Description("Enables switching between files such as ISomething.cs and Something.cs")]
        public bool EnableSwitchBetweenInterfaceAndImplementation
        {
            get { return enableSwitchBetweenInterfaceAndImplementation; }
            set { enableSwitchBetweenInterfaceAndImplementation = value; }
        }

        [Category("Switching")]
        [DisplayName("Switch between Designer and Code-Behind")]
        [Description("Enables switching between code-behind and designer files")]
        public bool EnableSwitchBetweenDesignerAndCodeBehind
        {
            get { return enableSwitchBetweenDesignerAndCodeBehind; }
            set { enableSwitchBetweenDesignerAndCodeBehind = value; }
        }
        
        [Category("Switching")]
        [DisplayName("Extensions")]
        [Description("File Extensions which can be switched between")]
        public List<ExtensionSwitch> ExtensionSwitches
        {
            get { return extensionSwitches; }
            set { extensionSwitches = value; }
        }
    }
}
