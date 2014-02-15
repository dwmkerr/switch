using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchCore.Configuration
{
    public interface ISwitchConfiguration
    {
        bool EnableSwitchBetweenInterfaceAndImplementation { get; set; }
        bool EnableSwitchBetweenDesignerAndCodeBehind { get; set; }
        List<ExtensionSwitch>  ExtensionSwitches { get; set; } 
    }
    public interface IExtensionSwitch
    {
        string From { get; set; }
        string To { get; set; }
    }

    [Serializable]
    public class SwitchConfiguration : ISwitchConfiguration
    {
        [XmlElement("EnableSwitchBetweenInterfaceAndImplementation")]
        public bool EnableSwitchBetweenInterfaceAndImplementation { get; set; }

        [XmlElement("EnableSwitchBetweenDesignerAndCodeBehind")]
        public bool EnableSwitchBetweenDesignerAndCodeBehind { get; set; }

        [XmlArray("ExtensionSwitches")]
        public List<ExtensionSwitch> ExtensionSwitches { get; set; } 
    }
}
