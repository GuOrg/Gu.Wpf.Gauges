namespace Gu.Wpf.Gauges.Tests.TestHelpers
{
    using System.Drawing;
    using System.IO;
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

        private static BitmapSource CreateBitmapSource(System.Drawing.Bitmap bitmap)
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
    }
}
