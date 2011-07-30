using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DynamicConfigurationBuilder.MethodStrategies;

namespace DynamicConfigurationBuilder
{
    public class ConfigurationBuilder: DynamicObject
    {
        private readonly DynamicConfiguration _built;
        private readonly IList<IMethodStrategy> _strategies;

        public ConfigurationBuilder(DynamicConfiguration built, IEnumerable<IMethodStrategy> strategies)
        {
            if(built == null)
                throw new ArgumentException("You must specify a DynamicConfiguration object to be built.");

            if(strategies == null)
                throw new ArgumentException("You Must specify collection of IMethodStrategy to deal with dynamic method invocation.");

            _built = built;

            _strategies = new List<IMethodStrategy>(strategies);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methodName = binder.Name;

            var strategy = (from s in _strategies
                            where s.IsMatch(methodName)
                            select s).FirstOrDefault();

            if (strategy == null)
                throw new ArgumentException(string.Format("The method strategy for member name {0} was not found.", methodName));

            strategy.Execute(methodName, _built, args);

            result = this;

            return true;
        }
    }
}