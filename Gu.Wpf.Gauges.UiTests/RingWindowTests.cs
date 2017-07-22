namespace Gu.Wpf.Gauges.UiTests
{
    using System;
    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.UIA3;
    using Gu.Wpf.Gauges.UiTests.Helpers;
    using NUnit.Framework;

    public sealed class RingWindowTests : IDisposable
    {
        private readonly Application app;
        private readonly UIA3Automation automation;
        private Window window;

        public RingWindowTests()
        {
            this.app = Application.Launch(Info.CreateStartInfo("RingWindow"));
            this.automation = new UIA3Automation();
            this.window = this.app.GetMainWindow(this.automation);
        }

        [Test]
        public void Loads()
        {
            Assert.Pass("Just checking that it loads for now");
        }

        public void Dispose()
        {
            this.app?.Dispose();
            this.automation?.Dispose();
        }
    }
}