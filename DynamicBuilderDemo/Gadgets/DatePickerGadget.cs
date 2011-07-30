using System;
using System.Web.Mvc;
using DynamicConfigurationBuilder;

namespace DynamicBuilderDemo.Gadgets
{
    public class DatePickerGadget : Gadget
    {
        public DatePickerGadget(Action<ConfigurationBuilder> configurationAction) 
            : base(configurationAction)
        {
        }

        protected override MvcHtmlString Render()
        {
            return new MvcHtmlString(string.Format("<input type='text' id='{0}' value='{1}' class='{2}'/>",
                     Configuration.Id,
                     Configuration.Value,
                     Configuration.Class + " gm-DatePicker"));
        }
    }
}