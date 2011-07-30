using System.Collections.Generic;
using DynamicConfigurationBuilder.MethodStrategies;

namespace DynamicBuilderDemo.Gadgets.Configuration
{
    public class OptionSetter : IMethodStrategy
    {
        public bool IsMatch(string methodName)
        {
            return methodName.StartsWith("HasOption") && methodName.EndsWith("WithValue");
        }

        public void Execute(string methodName, dynamic config, params object[] args)
        {
            if(config.Options == null)
                config.Options = new List<dynamic>();

            var displayName = methodName.Replace("HasOption", "").Replace("WithValue", "");
            config.Options.Add(new { Display = displayName, Value = (string) args[0] });
        }
    }
}