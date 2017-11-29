namespace Gu.Wpf.Gauges.UiTests
{
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public class AngularTickBarWindowTests
    {
        [Test]
        public void Loads()
        {
            using (var app = Application.Launch("Gu.Wpf.Gauges.Sample.exe", "AngularTickBarWindow"))
            {
                app.WaitForMainWindow();
                Assert.Pass("Just checking that it loads for now");
            }
        }
    }
}