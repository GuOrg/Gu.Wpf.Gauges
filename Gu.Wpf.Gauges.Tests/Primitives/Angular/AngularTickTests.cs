namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using NUnit.Framework;

    public partial class AngularTickTests
    {
        public class CreatTickTests
        {
            [TestCase(-90, 269)]
            [TestCase(-90, 269.999)]
            [TestCase(-90, 270)]
            [TestCase(-90, 360)]
            public void CreateTickTests(double start, double end)
            {
                var arcInfo = new ArcInfo(new Point(100, 100), 80, Angle.FromDegrees(start), Angle.FromDegrees(end));
                var result = AngularTick.CreateArcPathFigure(arcInfo, Angle.FromDegrees(start), Angle.FromDegrees(end), 10, 0);
                var path = result.ToString(CultureInfo.InvariantCulture).Replace(";", " ");
                Console.WriteLine(path);
            }
        }
    }

    [Apartment(ApartmentState.STA)]
    public partial class AngularTickTests
    {
        private static readonly IReadOnlyList<TestCase> RenderCases = TestCase.Create(
                                                                                  thicknesses: new[] { 10, double.PositiveInfinity },
                                                                                  values: new[] { double.NaN, 0, 5, 10 },
                                                                                  tickWidths: new[] { 1.0, 5 },
                                                                                  strokeThicknesses: new[] { 0.0, 1 },
                                                                                  paddings: new[] { default(Thickness) })
                                                                              .ToArray();

        private static readonly IReadOnlyList<TestCase> RenderWithPaddingCases = TestCase.Create(
                                                                                             thicknesses: new[] { 10, double.PositiveInfinity },
                                                                                             values: new[] { 5.0 },
                                                                                             tickWidths: new[] { 5.0 },
                                                                                             strokeThicknesses: new[] { 1.0 },
                                                                                             paddings: new[] { new Thickness(1, 2, 3, 4), })
                                                                                         .ToArray();

        [TestCaseSource(nameof(RenderCases))]
        public void Render(TestCase testCase)
        {
            var tickBar = new AngularTick
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickShape = testCase.TickShape,
                              Fill = Brushes.Red,
                              TickWidth = testCase.TickWidth,
                              Thickness = testCase.Thickness,
                              Stroke = Brushes.Black,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                          };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCaseSource(nameof(RenderWithPaddingCases))]
        public void RenderWithPadding(TestCase testCase)
        {
            var tickBar = new AngularTick
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickShape = testCase.TickShape,
                              Fill = Brushes.Red,
                              TickWidth = testCase.TickWidth,
                              Thickness = testCase.Thickness,
                              Stroke = Brushes.Black,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                          };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,2")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 1, "0 0 0 1", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 1 0 1", "0,1,0,1")]
        public void Overflow(bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new AngularTick
                          {
                              StrokeThickness = strokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              TickWidth = tickWidth,
                              Stroke = Brushes.Black,
                              Fill = Brushes.Red,
                              IsDirectionReversed = isDirectionReversed,
                              Padding = padding.AsThickness(),
                          };

            var gauge = new AngularGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
        }

        private static string GetFileName(AngularTick tickBar)
        {
            if (double.IsNaN(tickBar.Value))
            {
                return "AngularTick_Value_NaN.png";
            }

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding.ToString().Replace(",", "_")}";

            var thickness = double.IsInfinity(tickBar.Thickness) ? "inf" : tickBar.Thickness.ToString(CultureInfo.InvariantCulture);
            return $@"AngularTick_Value_{tickBar.Value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_TickShape_{tickBar.TickShape}_StrokeThickness_{tickBar.StrokeThickness}_Thickness_{thickness}.png"
                .Replace(" ", "_");
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(AngularTick tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\AngularTick");
            tickBar.SaveImage(new Size(100, 100), $@"C:\Temp\AngularTick\{GetFileName(tickBar)}");
        }

        public class TestCase
        {
            public TestCase(
                bool isDirectionReversed,
                double thickness,
                double value,
                TickShape tickShape,
                double tickWidth,
                double strokeThickness,
                Thickness padding)
            {
                this.IsDirectionReversed = isDirectionReversed;
                this.Thickness = thickness;
                this.Value = value;
                this.TickShape = tickShape;
                this.TickWidth = tickWidth;
                this.StrokeThickness = strokeThickness;
                this.Padding = padding;
            }

            public bool IsDirectionReversed { get; }

            public double Thickness { get; }

            public double Value { get; }

            public TickShape TickShape { get; }

            public double TickWidth { get; }

            public double StrokeThickness { get; }

            public Thickness Padding { get; }

            public static IEnumerable<TestCase> Create(
                double[] thicknesses,
                double[] values,
                double[] tickWidths,
                double[] strokeThicknesses,
                Thickness[] paddings)
            {
                foreach (var isDirectionReversed in new[] { true, false })
                {
                    foreach (var thickness in thicknesses)
                    {
                        foreach (var value in values)
                        {
                            foreach (var tickShape in new[] { TickShape.Arc, TickShape.Rectangle })
                            {
                                foreach (var tickWidth in tickWidths)
                                {
                                    foreach (var strokeThickness in strokeThicknesses)
                                    {
                                        foreach (var padding in paddings)
                                        {
                                            yield return new TestCase(
                                                isDirectionReversed,
                                                thickness,
                                                value,
                                                tickShape,
                                                tickWidth,
                                                strokeThickness,
                                                padding);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                return $"{nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.Thickness)}: {this.Thickness}, {nameof(this.Value)}: {this.Value}, {nameof(this.TickShape)}: {this.TickShape}, {nameof(this.TickWidth)}: {this.TickWidth}, {nameof(this.StrokeThickness)}: {this.StrokeThickness}, {nameof(this.Padding)}: {this.Padding}";
            }
        }
    }
}