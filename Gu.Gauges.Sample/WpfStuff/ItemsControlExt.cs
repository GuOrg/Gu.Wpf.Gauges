namespace Gu.Gauges.Sample
{
    using System;
    using System.Windows;

    public static class ItemsControlExt
    {
        public static readonly DependencyProperty EnumSourceProperty = DependencyProperty.RegisterAttached(
            "EnumSource",
            typeof(Type),
            typeof(ItemsControlExt),
            new PropertyMetadata(default(Type), PropertyChangedCallback));

        public static void SetEnumSource(DependencyObject element, Type value)
        {
            element.SetValue(EnumSourceProperty, value);
        }

        public static Type GetEnumSource(DependencyObject element)
        {
            return (Type)element.GetValue(EnumSourceProperty);
        }

        private static void PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = o as System.Windows.Controls.ItemsControl;
            if (itemsControl == null)
            {
                return;
            }
            var type = e.NewValue as Type;
            if (type == null)
            {
                return;
            }
            if (!type.IsEnum)
            {
                throw new ArgumentException("Expecting an enum type");
            }
            itemsControl.ItemsSource = Enum.GetValues(type);
        }
    }
}
