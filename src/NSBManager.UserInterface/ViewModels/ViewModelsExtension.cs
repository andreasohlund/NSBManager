using System;
using System.Linq.Expressions;

namespace NSBManager.UserInterface.ViewModels
{
    public static class ViewModelsExtension
    {
        public static void RaisePropertyChanged<T, TProperty>(this T baseViewModel, Expression<Func<T, TProperty>> expression) where T : BaseViewModel
        {
            baseViewModel.RaisePropertyChanged(baseViewModel.GetPropertyName(expression));
        }

        public static string GetPropertyName<T, TProperty>(this T owner, Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                var propertyName = memberExpression.Member.Name;
                return propertyName;
            }
            //Todo: Exception handling if empty? Throw notImplemented?
            return string.Empty;
        }
    }
}