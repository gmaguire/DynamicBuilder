using System;
using System.Collections.Generic;

namespace DynamicConfigurationBuilder.MethodStrategies
{
    public class ListItemSetter : IMethodStrategy
    {
        private const string MethodPrefix = "Has";
        private const string MethodSuffix = "ListItem";

        public bool IsMatch(string methodName)
        {
            if (methodName.StartsWith(MethodPrefix) && methodName.EndsWith(MethodSuffix))
            {
                var propertyName = GetRequestedPropertyName(methodName);
                if (!string.IsNullOrWhiteSpace(propertyName))
                    return true;
            }

            return false;
        }

        public void Execute(string methodName, dynamic config, params object[] args)
        {
            var propertyName = GetRequestedPropertyName(methodName);

            var list = GetListFromConfig(config, propertyName);

            if (args == null || args.Length != 1)
                throw new ArgumentException("You must specify exactly 1 parameter");

            object value = args[0];
            list.Add(value);
        }

        private static List<dynamic> GetListFromConfig(dynamic config, string propertyName)
        {
            var list = config[propertyName] as List<dynamic>;

            if (list == null)
            {
                list = new List<dynamic>();
                config[propertyName] = list;
            }

            return list;
        }

        private static string GetRequestedPropertyName(string methodName)
        {
            return methodName.Replace(MethodPrefix, "").Replace(MethodSuffix, "");
        }
    }
}
