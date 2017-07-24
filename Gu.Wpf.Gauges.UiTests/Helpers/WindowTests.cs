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

        protected WindowTests(string windowName)
        {
            this.app = Application.Launch(Info.CreateStartInfo(windowName));
            this.automation = new UIA3Automation();
            this.Window = this.app.GetMainWindow(this.automation);
        }

        public Window Window { get; }

        public void Dispose()
        {
            this.app?.Dispose();
            this.automation?.Dispose();
        }
    }
}