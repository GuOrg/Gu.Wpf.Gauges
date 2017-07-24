namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class AngularBlockBarTests
    {
        private static readonly IReadOnlyList<TestCase> RenderCases = TestCase.Create(
                                                                                  thicknesses: new[] { 10.0 },
                                                                                  values: new[] { double.NaN, 0, 5, 10 },
                                                                                  tickGaps: new[] { 1.0, 5 },
                                                                                  strokeThicknesses: new[] { 0.0, 1 },
                                                                                  tickFrequencies: new[] { 0, 5.0 },
                                                                                  tickCollections: new[] { null, (DoubleCollection)new DoubleCollection(new[] { 1.0, 2, 6 }).GetCurrentValueAsFrozen() },
                                                                                  paddings: new[] { default(Thickness) })
                                                                              .Where(x => !(x.TickFrequency <= 0 && x.Ticks == null))
                                                                              .ToArray();

        private static readonly IReadOnlyList<TestCase> RenderWithPaddingCases = TestCase.Create(
                                                                                             thicknesses: new[] { 10.0 },
                                                                                             values: new[] { double.NaN },
                                                                                             tickGaps: new[] { 5.0 },
                                                                                             strokeThicknesses: new[] { 1.0 },
                                                                                             tickFrequencies: new[] { 1.0 },
                                                                                             tickCollections: new[] { (DoubleCollection)new DoubleCollection(new[] { 1.5, }).GetCurrentValueAsFrozen() },
                                                                                             paddings: new[] { new Thickness(1, 2, 3, 4), })
                                                                                         .ToArray();

        [TestCaseSource(nameof(RenderCases))]
        public void Render(TestCase testCase)
        {
            var tickBar = new AngularBlockBar
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickShape = testCase.TickShape,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              Fill = Brushes.Red,
                              TickGap = testCase.TickGap,
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
            var tickBar = new AngularBlockBar
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickShape = testCase.TickShape,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              Fill = Brushes.Red,
                              TickGap = testCase.TickGap,
                              Thickness = testCase.Thickness,
                              Stroke = Brushes.Black,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                          };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 1, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 1, "0 1 0 1", "0,1,0,1")]
        public void Overflow(bool isDirectionReversed, double tickGap, double strokeThickness, string padding, string expected)
        {
            var tickBar = new AngularBlockBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                TickGap = tickGap,
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

        private static string GetFileName(AngularBlockBar tickBar)
        {
            if (DoubleUtil.AreClose(tickBar.Value, 0))
            {
                return "AngularBlockBar_Value_0.png";
            }

            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks.ToString(CultureInfo.InvariantCulture)}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding.ToString().Replace(",", "_")}";

            var value = double.IsNaN(tickBar.Value) ||
                        DoubleUtil.AreClose(tickBar.Value, tickBar.Maximum)
                ? string.Empty
                : $"_Value_{tickBar.Value}";
            var thickness = double.IsInfinity(tickBar.Thickness) ? "inf" : tickBar.Thickness.ToString(CultureInfo.InvariantCulture);
            return $@"AngularBlockBar{value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickGap_{tickBar.TickGap}_TickShape_{tickBar.TickShape}_StrokeThickness_{tickBar.StrokeThickness}_Thickness_{thickness}{tickFrequency}{ticks}.png"
                .Replace(" ", "_");
        }

        private static void SaveImage(AngularBlockBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\AngularBlockBar");
            tickBar.SaveImage(new Size(100, 100), $@"C:\Temp\AngularBlockBar\{GetFileName(tickBar)}");
        }

        public class TestCase
        {
            public TestCase(
                bool isDirectionReversed,
                double thickness,
                double value,
                TickShape tickShape,
                double tickGap,
                double strokeThickness,
                double tickFrequency,
                DoubleCollection ticks,
                Thickness padding)
            {
                this.IsDirectionReversed = isDirectionReversed;
                this.Thickness = thickness;
                this.Value = value;
                this.TickShape = tickShape;
                this.TickGap = tickGap;
                this.StrokeThickness = strokeThickness;
                this.TickFrequency = tickFrequency;
                this.Ticks = ticks;
                this.Padding = padding;
            }

            public bool IsDirectionReversed { get; }

            public double Thickness { get; }

            public double Value { get; }

            public TickShape TickShape { get; }

            public double TickGap { get; }

            public double StrokeThickness { get; }

            public double TickFrequency { get; }

            public DoubleCollection Ticks { get; }

            public Thickness Padding { get; }

            public static IEnumerable<TestCase> Create(
                double[] thicknesses,
                double[] values,
                double[] tickGaps,
                double[] strokeThicknesses,
                double[] tickFrequencies,
                DoubleCollection[] tickCollections,
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
                                foreach (var tickWidth in tickGaps)
                                {
                                    foreach (var strokeThickness in strokeThicknesses)
                                    {
                                        foreach (var tickFrequency in tickFrequencies)
                                        {
                                            foreach (var ticks in tickCollections)
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
                                                        tickFrequency,
                                                        ticks,
                                                        padding);
                                                }
                                            }
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
                return $"{nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.Thickness)}: {this.Thickness}, {nameof(this.Value)}: {this.Value}, {nameof(this.TickShape)}: {this.TickShape}, {nameof(this.TickGap)}: {this.TickGap}, {nameof(this.StrokeThickness)}: {this.StrokeThickness}, {nameof(this.TickFrequency)}: {this.TickFrequency}, {nameof(this.Ticks)}: {this.Ticks}, {nameof(this.Padding)}: {this.Padding}";
            }
        }
    }
}