namespace Gu.Wpf.Gauges.UiTests
{
    using System;
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public class AngularBlockBarWindowTests
    {
        [Test]
        public void Loads()
        {
            using (var app = Application.Launch("Gu.Wpf.Gauges.Sample.exe", "AngularBlockBarWindow"))
            {
                app.WaitForMainWindow(TimeSpan.FromSeconds(10));
                Assert.Pass("Just checking that it loads for now");
            }
        }
    }
}
