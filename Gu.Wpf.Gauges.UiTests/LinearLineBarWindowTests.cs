namespace Gu.Wpf.Gauges.UiTests
{
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public sealed class LinearLineBarWindowTests
    {
        [Test]
        public void Loads()
        {
            using (var app = Application.Launch("Gu.Wpf.Gauges.Sample.exe", "LinearLineBarWindow"))
            {
                app.WaitForMainWindow();
                Assert.Pass("Just checking that it loads for now");
            }
        }
    }
}