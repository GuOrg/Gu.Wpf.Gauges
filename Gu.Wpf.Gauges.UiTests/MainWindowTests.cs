namespace Gu.Wpf.Gauges.UiTests
{
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public class MainWindowTests
    {
        [Test]
        public void ClickAllTabs()
        {
            // Just a smoke test so that we do not explode.
            using (var app = Application.Launch("Gu.Wpf.Gauges.Sample.exe"))
            {
                var window = app.MainWindow;
                var tab = window.FindTabControl();
                foreach (var item in tab.Items)
                {
                    item.Select();
                }
            }
        }
    }
}