using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwitchCore.Configuration;
using SwitchCore.SwitchTargets;
using EnvDTE80;

namespace SwitchCore
{
    public static class SwitchCommand
    {
        public static void Execute(DTE2 applicationObject, ISwitchConfiguration configuration)
        {
            //  If we have no application or no document, we're done here.
            if (applicationObject == null || applicationObject.ActiveDocument == null)
                return;
            foreach (var target in BuildSwitchTargets(configuration))
                if (target.DoSwitch(applicationObject, applicationObject.ActiveDocument))
                    break;
        }
        
        /// <summary>
        /// Builds the switch targets.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<ISwitchTarget> BuildSwitchTargets(ISwitchConfiguration configuration)
        {
            if (configuration.EnableSwitchBetweenDesignerAndCodeBehind)
                yield return new DesignViewSwitchTarget();
            if (configuration.EnableSwitchBetweenInterfaceAndImplementation)
                yield return new InterfaceSwitchTarget();
            foreach (var extensionSwitch in configuration.ExtensionSwitches)
                yield return new ExtensionSwitchTarget(extensionSwitch.From, extensionSwitch.To);
        }
    }
}
