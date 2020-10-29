namespace Gu.Wpf.Gauges.Tests
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using NUnit.Framework;

    using PixelFormat = System.Windows.Media.PixelFormat;
    using Size = System.Windows.Size;

    public static class ImageAssert
    {
        public static void AreEqual(string fileName, UIElement tickBar)
        {
            var assembly = typeof(ImageAssert).Assembly;
            var name = assembly.GetManifestResourceNames()
                               .SingleOrDefault(x => x.EndsWith(fileName, ignoreCase: true, culture: CultureInfo.InvariantCulture));
            Assert.NotNull(name, $"Did not find a resource named {fileName}");
            using var stream = assembly.GetManifestResourceStream(name);
            Assert.NotNull(stream);
            using var expected = (Bitmap)Image.FromStream(stream);
            using var actual = tickBar.ToBitmap(expected.Size(), expected.PixelFormat());
            try
            {
                Gu.Wpf.UiAutomation.ImageAssert.AreEqual(expected, actual);
            }
            catch
            {
                var fullFileName = Path.Combine(Path.GetTempPath(), fileName);
                //// ReSharper disable once AssignNullToNotNullAttribute
                _ = Directory.CreateDirectory(Path.GetDirectoryName(fullFileName));
                if (File.Exists(fullFileName))
                {
                    File.Delete(fullFileName);
                }

                actual.Save(fullFileName);
                TestContext.AddTestAttachment(fullFileName);
                throw;
            }
        }

        public static Bitmap ToBitmap(this UIElement element, Size size, PixelFormat pixelFormat)
        {
            return element.ToRenderTargetBitmap(size, pixelFormat)
                          .ToBitmap();
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this UIElement element, Size size, PixelFormat pixelFormat)
        {
            var result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, pixelFormat);
            element.Measure(size);
            element.Arrange(new Rect(size));
            result.Render(element);
            return result;
        }

        public static Bitmap ToBitmap(this RenderTargetBitmap bitmap)
        {
            using var stream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
            return new Bitmap(stream);
        }

        public static void SaveImage(this UIElement element, Size size, string fileName)
        {
            SaveImage(element, size, GetPixelFormat(fileName), fileName);
        }

        public static void SaveImage(this UIElement element, Size size, PixelFormat pixelFormat, string fileName)
        {
            using var stream = File.OpenWrite(fileName);
            element.Measure(size);
            element.Arrange(new Rect(size));
            var renderTargetBitmap = element.ToRenderTargetBitmap(size, pixelFormat);
            var encoder = GetEncoder(fileName);
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            encoder.Save(stream);
        }

        private static BitmapEncoder GetEncoder(string fileName)
        {
            if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.OrdinalIgnoreCase))
            {
                return new PngBitmapEncoder();
            }

            throw new ArgumentException($"Cannot save {Path.GetExtension(fileName)}");
        }

        private static PixelFormat GetPixelFormat(string fileName)
        {
            if (string.Equals(Path.GetExtension(fileName), ".png", StringComparison.OrdinalIgnoreCase))
            {
                return PixelFormats.Pbgra32;
            }

            throw new ArgumentException($"Cannot save {Path.GetExtension(fileName)}");
        }

        private static Size Size(this Image bitmap)
        {
            return new Size(bitmap.Width, bitmap.Height);
        }

        private static PixelFormat PixelFormat(this Image bitmap)
        {
            switch (bitmap.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return PixelFormats.Pbgra32;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
