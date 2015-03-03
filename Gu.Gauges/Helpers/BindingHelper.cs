namespace Gu.Gauges
{
    using System;
    using System.Linq.Expressions;
    using System.Windows;
    using System.Windows.Data;

    internal static class BindingHelper
    {
        public static BindingExpressionBase BindOneWay(
            DependencyObject source,
            Expression<Func<object>> prop,
            DependencyObject target,
            DependencyProperty targetProp)
        {
            var property = NameOf.Property(prop);
            var binding = new Binding(property)
            {
                Source = source,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            var bindingExpression = BindingOperations.SetBinding(target, targetProp, binding);
            return bindingExpression;
        }

        public static BindingExpressionBase BindOneWay(
    DependencyObject source,
    string path,
    DependencyObject target,
    DependencyProperty targetProp)
        {
            var binding = new Binding(path)
            {
                Source = source,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            var bindingExpression = BindingOperations.SetBinding(target, targetProp, binding);
            return bindingExpression;
        }
    }
}
