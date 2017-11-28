// ReSharper disable PossibleNullReferenceException
namespace Gu.Wpf.Gauges.Tests
{
    using NUnit.Framework;

    public class AngleTypeConverterTests
    {
        [TestCase(1, 1)]
        [TestCase(1.2, 1.2)]
        [TestCase("1.2", 1.2)]
        [TestCase("1.2°", 1.2)]
        [TestCase("1.2°", 1.2)]
        [TestCase("1.2 deg", 1.2)]
        [TestCase("1.2 Deg", 1.2)]
        [TestCase("1.2 degrees", 1.2)]
        public void ConvertFromDegrees(object value, double expected)
        {
            var converter = new AngleTypeConverter();
            Assert.AreEqual(true, converter.CanConvertFrom(null, value.GetType()));
            Assert.AreEqual(expected, ((Angle)converter.ConvertFrom(null, null, value)).Degrees);
        }

        [TestCase("1.2 rad", 1.2)]
        [TestCase("1.2 Rad", 1.2)]
        [TestCase("1.2 radians", 1.2)]
        public void ConvertFromRadians(object value, double expected)
        {
            var converter = new AngleTypeConverter();
            Assert.AreEqual(true, converter.CanConvertFrom(null, value.GetType()));
            Assert.AreEqual(expected, ((Angle)converter.ConvertFrom(null, null, value)).Radians);
        }
    }
}
