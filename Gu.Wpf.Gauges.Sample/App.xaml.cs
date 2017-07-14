namespace Gu.Wpf.Gauges.Sample
{
    using System;
    using System.Windows;

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length == 1)
            {
                var window = e.Args[0];
                this.StartupUri = window == "MainWindow"
                    ? new Uri($"{window}.xaml", UriKind.Relative)
                    : new Uri($"Sandbox/{window}.xaml", UriKind.Relative);
            }

            base.OnStartup(e);
        }
    }
}