namespace Gu.Wpf.Gauges.UiTests
{
    using System;
    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.UIA3;

    public abstract class WindowTests : IDisposable
    {
        private readonly Application app;
        private readonly UIA3Automation automation;
        private Window window;

        protected WindowTests(string windowName)
        {
            this.app = Application.Launch(Info.CreateStartInfo(windowName));
            this.automation = new UIA3Automation();
            this.window = this.app.GetMainWindow(this.automation);
        }

        public void Dispose()
        {
            this.app?.Dispose();
            this.automation?.Dispose();
        }
    }
}