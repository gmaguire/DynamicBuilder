using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using DynamicBuilderDemo.Gadgets.Configuration;
using DynamicConfigurationBuilder;
using DynamicConfigurationBuilder.MethodStrategies;

namespace DynamicBuilderDemo.Gadgets
{
    public class DropdownListGadget : Gadget
    {
        public DropdownListGadget(Action<ConfigurationBuilder> configurationAction)
            : base(configurationAction, new List<IMethodStrategy>(){ new OptionSetter() })
        {
        }

        protected override MvcHtmlString Render()
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.AppendFormat("<select id='{0}' class='{1}' onclick=\"{2}\">", 
                                        Configuration.Id,
                                        Configuration.Class,
                                        Configuration.OnClickHandler);

            foreach (var option in Configuration.Options)
            {
                htmlBuilder.AppendFormat("<option value='{0}'>{1}</option>", option.Value, option.Display);
            }

            htmlBuilder.Append("</select>");

            return new MvcHtmlString(htmlBuilder.ToString());
        }
    }
}