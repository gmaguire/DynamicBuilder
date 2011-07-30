using System;
using System.Web.Mvc;
using DynamicConfigurationBuilder;

namespace DynamicBuilderDemo.Gadgets
{
    public class TextBoxGadget : Gadget
    {
        public TextBoxGadget(Action<ConfigurationBuilder> configurationAction) 
            : base(configurationAction)
        {
        }

        protected override MvcHtmlString Render()
        {
            var classes = string.Join(" ", Configuration.Classes.ToArray());

            return new MvcHtmlString(string.Format("<input type='text' id='{0}' value='{1}' class='{2}' maxlength='{3}' />",
                                                    Configuration.Id,
                                                    Configuration.Value,
                                                    classes,
                                                    Configuration.MaxLength ?? ""));
        }
    }
}