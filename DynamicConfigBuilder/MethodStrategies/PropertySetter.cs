using System;

namespace DynamicConfigurationBuilder.MethodStrategies
{
    public class PropertySetter : IMethodStrategy
    {
        private const string MethodPrefix = "Has";
        private const string MethodSuffix = "SetTo";

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
            var propertyName = methodName.Replace(MethodPrefix, string.Empty).Replace(MethodSuffix, string.Empty);

            if(args == null || args.Length != 1)
                throw new ArgumentException("You must specify exactly 1 parameter");

            object value = args[0];
            config[propertyName] = value;
        }

        private static string GetRequestedPropertyName(string methodName)
        {
            return methodName.Replace(MethodPrefix, "").Replace(MethodSuffix, "");
        }
    }
}