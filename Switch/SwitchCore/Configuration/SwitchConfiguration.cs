using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchCore.Configuration
{
    [Serializable]
    public class SwitchConfiguration
    {
        [XmlElement("EnableSwitchBetweenInterfaceAndImplementation")]
        public bool EnableSwitchBetweenInterfaceAndImplementation { get; set; }

        [XmlElement("EnableSwitchBetweenDesignerAndCodeBehind")]
        public bool EnableSwitchBetweenDesignerAndCodeBehind { get; set; }

        [XmlArray("ExtensionSwitches")]
        public List<ExtensionSwitch> ExtensionSwitches { get; set; } 
    }
}
