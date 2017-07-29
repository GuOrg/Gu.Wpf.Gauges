namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;
    using System.Text.RegularExpressions;

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
        private static readonly MethodInfo FromDegreesMethod = typeof(Angle).GetMethod(nameof(Angle.FromDegrees), BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(double) }, null);

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
            if (value is string text)
            {
                if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double d1))
                {
                    return Angle.FromDegrees(d1);
                }

                var match = Regex.Match(text, "(?<number>.+) ?(°|deg|degrees)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                if (match.Success &&
                    double.TryParse(
                        match.Groups["number"].Value,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double d2))
                {
                    return Angle.FromDegrees(d2);
                }

                match = Regex.Match(text, "(?<number>.+) ?(rad|radians)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                if (match.Success &&
                    double.TryParse(
                        match.Groups["number"].Value,
                        NumberStyles.Float,
                        CultureInfo.InvariantCulture,
                        out double d3))
                {
                    return Angle.FromRadians(d3);
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
            if (value is Angle && 
                destinationType != null)
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