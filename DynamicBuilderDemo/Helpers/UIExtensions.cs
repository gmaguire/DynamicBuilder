using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using DynamicBuilderDemo.Gadgets;

namespace DynamicBuilderDemo.Helpers
{
    public static class UIExtensions
    {
        public static dynamic CustomTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
                                                                    Expression<Func<TModel,TProperty>> expression,
                                                                    Action<dynamic> builder)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var gadget = new TextBoxGadget(builder);
            gadget.Configuration.Id = propertyName;
            
            return gadget.RenderHtml();

        }

        public static dynamic CustomDatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
                                                            Expression<Func<TModel, TProperty>> expression,
                                                            Action<dynamic> builder)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var gadget = new DatePickerGadget(builder);
            gadget.Configuration.Id = propertyName;

            return gadget.RenderHtml();

        }

        public static dynamic CustomDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
                                                                    Expression<Func<TModel, TProperty>> expression,
                                                                    Action<dynamic> builder)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var gadget = new DropdownListGadget(builder);
            gadget.Configuration.Id = propertyName;

            return gadget.RenderHtml();

        }
    }
}