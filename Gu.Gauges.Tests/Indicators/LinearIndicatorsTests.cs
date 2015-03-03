namespace Gu.Gauges.Tests.Indicators
{
    using NUnit.Framework;

    [RequiresSTA]
    public class LinearIndicatorsTests
    {
        [Test]
        public void BindsPanelToAxis()
        {
            var indicators = new LinearIndicators();
            Assert.AreEqual(1, indicators.Panel.Maximum);
            indicators.Gauge = new LinearGauge();
            Assert.AreEqual(1, indicators.Panel.Maximum);
            indicators.Gauge.Axis = new LinearAxis();
            Assert.AreEqual(1, indicators.Panel.Maximum);
            indicators.Gauge.Axis.Maximum = 2;
            Assert.AreEqual(2, indicators.Panel.Maximum);
        }

        [Test]
        public void BindsPanelToAxisWhenGivenACompleteGauge()
        {
            var indicators = new LinearIndicators();
            Assert.AreEqual(1, indicators.Panel.Maximum);
            indicators.Gauge = new LinearGauge { Axis = new LinearAxis { Maximum = 2 } };
            Assert.AreEqual(2, indicators.Panel.Maximum);
        }
    }
}