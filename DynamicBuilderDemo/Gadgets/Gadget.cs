using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DynamicConfigurationBuilder;
using DynamicConfigurationBuilder.MethodStrategies;

namespace DynamicBuilderDemo.Gadgets
{
    public abstract class Gadget : IDynamicallyConfigurable
    {
        public dynamic Configuration { get; set; }

        protected Gadget(Action<ConfigurationBuilder> configurationAction, IEnumerable<IMethodStrategy> additionalethodStrategies  = null)
        {
            Configuration = new DynamicConfiguration();

            var methodStrategies = new List<IMethodStrategy>()
                                                {
                                                    new PropertySetter(),
                                                    new ListItemSetter()
                                                };

            if (additionalethodStrategies != null)
                methodStrategies.AddRange(additionalethodStrategies);

            configurationAction(new ConfigurationBuilder(Configuration, methodStrategies));
        }

        public MvcHtmlString RenderHtml()
        {
            return Render();
        }

        protected abstract MvcHtmlString Render();
    }
}