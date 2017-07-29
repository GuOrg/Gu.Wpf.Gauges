namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.
    /// </summary>
    /// <devdoc>
    /// <para>Provides a type converter to convert <see cref='Angle'/>
    /// objects to and from various
    /// other representations.</para>
    /// </devdoc>
    public class AngleTypeConverter : TypeConverter
    {
        private static readonly MethodInfo FromDegreesMethod = typeof(Angle).GetMethod(nameof(Angle.FromDegrees), BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(double) }, null);

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) ||
                sourceType == typeof(double) ||
                sourceType == typeof(int))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor) ||
                destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                if (Angle.TryParse(s, out Angle angle))
                {
                    return angle;
                }
            }

            if (value is double d)
            {
                return Angle.FromDegrees(d);
            }

            if (value is int i)
            {
                return Angle.FromDegrees(i);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Angle)
            {
                var angle = (Angle)value;
                if (destinationType == typeof(string))
                {
                    return angle.Degrees.ToString(culture);
                }

                if (destinationType == typeof(InstanceDescriptor))
                {
                    if (FromDegreesMethod != null)
                    {
                        var args = new object[] { angle.Degrees };
                        return new InstanceDescriptor(FromDegreesMethod, args);
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}