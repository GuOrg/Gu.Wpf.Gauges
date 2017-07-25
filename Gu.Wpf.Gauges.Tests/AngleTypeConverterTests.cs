namespace Gu.Wpf.Gauges.Tests
{
    using NUnit.Framework;

    public class AngleTypeConverterTests
    {
        [TestCase(1, 1)]
        [TestCase(1.0, 1)]
        [TestCase("1", 1)]
        public void ConvertFrom(object value, double expected)
        {
            var converter = new AngleTypeConverter();
            Assert.AreEqual(true, converter.CanConvertFrom(null, value.GetType()));
            Assert.AreEqual(expected, ((Angle)converter.ConvertFrom(null, null, value)).Degrees);
        }
    }
}
