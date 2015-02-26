namespace Gu.Gauges
{
    using System;
    using System.Windows;

    internal static class SizeExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="vertical"></param>
        /// <param name="horizontal"></param>
        /// <returns>A vector from topleft to the specified point</returns>
        internal static Vector Offset(this Size size, Vertical vertical, Horizontal horizontal)
        {
            var x = 0.0;
            var y = 0.0;
            switch (horizontal)
            {
                case Horizontal.Left:
                    x = 0;
                    break;
                case Horizontal.Center:
                    x = size.Width / 2;
                    break;
                case Horizontal.Right:
                    x = size.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("horizontal");
            }

            switch (vertical)
            {
                case Vertical.Top:
                    y = 0;
                    break;
                case Vertical.Mid:
                    y = size.Height / 2;
                    break;
                case Vertical.Bottom:
                    y = size.Height;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("vertical");
            }


            return new Vector(-x, -y);
        }
    }
}
