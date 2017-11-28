namespace Gu.Wpf.Gauges.Tests
{
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public partial class ImageCompareView : UserControl
    {
        public ImageCompareView(Bitmap expected, Bitmap actual)
        {
            this.Expected = CreateBitmapSource(expected);
            this.Actual = CreateBitmapSource(actual);
            this.InitializeComponent();
        }

        public BitmapSource Expected { get; }

        public BitmapSource Actual { get; }

        private static BitmapSource CreateBitmapSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void OnToggleClick(object sender, RoutedEventArgs e)
        {
            this.ActualOpacity.SetCurrentValue(System.Windows.Controls.Primitives.RangeBase.ValueProperty, (double)1);
            this.ExpectedOpacity.SetCurrentValue(System.Windows.Controls.Primitives.RangeBase.ValueProperty, (double)1);
            this.ActualVisible.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, !this.ActualVisible.IsChecked);
            this.ExpectedVisible.SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, !this.ActualVisible.IsChecked);
        }
    }
}
