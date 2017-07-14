namespace Gu.Wpf.Gauges.UiTests.Helpers
{
    using FlaUI.Core;
    using FlaUI.Core.Definitions;
    using FlaUI.UIA3;
    using NUnit.Framework;

    public class MainWindowTests
    {
        [Test]
        public void ClickAllTabs()
        {
            // Just a smoke test so that everything builds.
            using (var app = Application.Launch(Info.CreateStartInfo("MainWindow")))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);
                    var tab = window.FindFirstDescendant(x => x.ByControlType(ControlType.Tab));
                    foreach (var element in tab.FindAllChildren(x => x.ByControlType(ControlType.TabItem)))
                    {
                        var tabItem = element.AsTabItem();
                        tabItem.Click();
                    }
                }
            }
        }
    }
}