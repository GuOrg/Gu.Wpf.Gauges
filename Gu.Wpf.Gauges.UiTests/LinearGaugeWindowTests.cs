namespace Gu.Wpf.Gauges.UiTests
{
    using NUnit.Framework;

    public sealed class LinearGaugeWindowTests : WindowTests
    {
        public LinearGaugeWindowTests()
            : base("LinearGaugeWindow")
        {
        }

        [Test]
        public void Loads()
        {
            Assert.Pass("Just checking that it loads for now");
        }
    }
}