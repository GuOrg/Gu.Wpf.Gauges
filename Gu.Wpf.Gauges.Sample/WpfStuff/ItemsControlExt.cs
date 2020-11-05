namespace Gu.Wpf.Gauges.Sample
{
    using System;
    using System.Windows;

    public static class ItemsControlExt
    {
        public static readonly DependencyProperty EnumSourceProperty = DependencyProperty.RegisterAttached(
            "EnumSource",
            typeof(Type),
            typeof(ItemsControlExt),
            new PropertyMetadata(default(Type), OnEnumSourceChanged));

        public static void SetEnumSource(DependencyObject element, Type value)
        {
            element.SetValue(EnumSourceProperty, value);
        }

        public static Type GetEnumSource(DependencyObject element)
        {
            return (Type)element.GetValue(EnumSourceProperty);
        }

        private static void OnEnumSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = o as System.Windows.Controls.ItemsControl;
            if (itemsControl is null)
            {
                return;
            }

            var type = e.NewValue as Type;
            if (type is null)
            {
                return;
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("Expecting an enum type");
            }

            itemsControl.SetCurrentValue(System.Windows.Controls.ItemsControl.ItemsSourceProperty, Enum.GetValues(type));
        }
    }
}
