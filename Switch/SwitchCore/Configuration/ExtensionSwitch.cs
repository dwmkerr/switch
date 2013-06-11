using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchCore.Configuration
{
    [Serializable]
    public class ExtensionSwitch
    {
        public ExtensionSwitch()
        {
            
        }

        public ExtensionSwitch(string from, string to)
        {
            From = from;
            To = to;
        }

        [XmlAttribute("from")]
        public string From { get; set; }

        [XmlAttribute("to")]
        public string To { get; set; }
    }
}
