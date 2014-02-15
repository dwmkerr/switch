using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchCore.Configuration
{
    [Serializable]
    public class ExtensionSwitch : IExtensionSwitch
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
        [Category("Extension Switching")]
        [DisplayName("From")]
        [Description("The extension to switch from, such as 'cpp'.")]
        public string From { get; set; }

        [XmlAttribute("to")]
        [Category("Extension Switching")]
        [DisplayName("To")]
        [Description("The extension to switch to, such as 'h'.")]
        public string To { get; set; }
    }
}
