namespace Gu.Wpf.Gauges.Tests.TestHelpers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using NUnit.Framework;
    using Size = System.Windows.Size;

    public static class ImageAssert
    {
        public static void AreEqual(Bitmap expected, UIElement actual)
        {
            using (var actualBmp = actual.ToBitmap(expected.Size(), expected.PixelFormat()))
            {
                AreEqual(expected, actualBmp);
            }
        }

        public static void AreEqual(Bitmap expected, Bitmap actual)
        {
            Assert.AreEqual(expected.Size, actual.Size);
            for (var x = 0; x < expected.Size.Width; x++)
            {
                for (var y = 0; y < expected.Size.Height; y++)
                {
                    Assert.AreEqual(expected.GetPixel(x, y).Name, actual.GetPixel(x, y).Name);
                }
            }
        }

        public static Bitmap ToBitmap(this UIElement element, Size size, PixelFormat pixelFormat)
        {
            return element.ToRenderTargetBitmap(size, pixelFormat)
                          .ToBitmap();
        }

        public static Bitmap ToBitmap(this UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32)
                          .ToBitmap();
        }

        public static RenderTargetBitmap ToRenderTargetBitmap(this UIElement element, Size size)
        {
            return element.ToRenderTargetBitmap(size, PixelFormats.Pbgra32);
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
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
                return new Bitmap(stream);
            }
        }

        public static void SaveImage(this UIElement element, Size size, string fileName)
        {
            SaveImage(element, size, GetPixelFormat(fileName), fileName);
        }

        public static void SaveImage(this UIElement element, Size size, PixelFormat pixelFormat, string fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                var renderTargetBitmap = element.ToRenderTargetBitmap(size, pixelFormat);
                var encoder = GetEncoder(fileName);
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(stream);
            }
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
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                case System.Drawing.Imaging.PixelFormat.Indexed:
                case System.Drawing.Imaging.PixelFormat.Gdi:
                case System.Drawing.Imaging.PixelFormat.Alpha:
                case System.Drawing.Imaging.PixelFormat.PAlpha:
                case System.Drawing.Imaging.PixelFormat.Extended:
                case System.Drawing.Imaging.PixelFormat.Canonical:
                case System.Drawing.Imaging.PixelFormat.Undefined:
                case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                case System.Drawing.Imaging.PixelFormat.Max:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
