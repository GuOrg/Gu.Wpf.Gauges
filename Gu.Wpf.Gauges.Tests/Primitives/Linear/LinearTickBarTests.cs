namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearTickBarTests
    {
        private static readonly IReadOnlyList<TestCase> RenderCases = TestCase.Create(
                                                                                  values: new[] { double.NaN, 0, 5, 10 },
                                                                                  tickWidths: new[] { 1.0, 5 },
                                                                                  strokeThicknesses: new[] { 0.0, 1 },
                                                                                  tickFrequencies: new[] { 0, 5.0 },
                                                                                  tickCollections: new[] { null, (DoubleCollection)new DoubleCollection(new[] { 1.0, 2, 6 }).GetCurrentValueAsFrozen() },
                                                                                  paddings: new[] { default(Thickness) })
                                                                              .Where(x => !(x.TickFrequency <= 0 && x.Ticks == null))
                                                                              .ToArray();

        private static readonly IReadOnlyList<TestCase> RenderWithValueCases = TestCase.Create(
                                                                                           values: new[] { double.NaN, 5.96, 6, 6.05 },
                                                                                           tickWidths: new[] { 1.0, 5 },
                                                                                           strokeThicknesses: new[] { 0.0, 1 },
                                                                                           tickFrequencies: new[] { 0, 5.0 },
                                                                                           tickCollections: new[] { null, (DoubleCollection)new DoubleCollection(new[] { 1.0, 2, 6 }).GetCurrentValueAsFrozen() },
                                                                                           paddings: new[] { default(Thickness) })
                                                                                       .Where(x => !(x.TickFrequency <= 0 && x.Ticks == null))
                                                                                       .ToArray();

        [TestCaseSource(nameof(RenderCases))]
        public void Render(TestCase testCase)
        {
            var tickBar = new LinearTickBar
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              TickWidth = testCase.TickWidth,
                              Stroke = Brushes.Black,
                              Fill = Brushes.Red,
                              Placement = testCase.Placement,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                          };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCaseSource(nameof(RenderWithValueCases))]
        public void RenderWithValue(TestCase testCase)
        {
            var tickBar = new LinearTickBar
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              TickWidth = testCase.TickWidth,
                              Stroke = Brushes.Black,
                              Fill = Brushes.Red,
                              Placement = testCase.Placement,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                          };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, "1 0 1 0", "1,0,1,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new LinearTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            var gauge = new LinearGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(LinearTickBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var orientation = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var value = double.IsNaN(tickBar.Value) || DoubleUtil.AreClose(tickBar.Value, tickBar.Maximum)
                ? string.Empty
                : $"_Value_{tickBar.Value}";
            if (DoubleUtil.AreClose(tickBar.Value, 0))
            {
                return $"LinearTickBar_Value_0{orientation}.png";
            }

            return $@"LinearTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}{tickFrequency}{ticks}{orientation}.png"
                .Replace(" ", "_");
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearTickBar tickBar)
        {
            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(10, 100)
                : new Size(100, 10);
            Directory.CreateDirectory(@"C:\Temp\LinearTickBar");
            tickBar.SaveImage(size, $@"C:\Temp\LinearTickBar\{GetFileName(tickBar)}");
        }

        public class TestCase
        {
            public TestCase(
                TickBarPlacement placement,
                bool isDirectionReversed,
                double value,
                double tickWidth,
                double strokeThickness,
                double tickFrequency,
                DoubleCollection ticks,
                Thickness padding)
            {
                this.Placement = placement;
                this.IsDirectionReversed = isDirectionReversed;
                this.Value = value;
                this.TickWidth = tickWidth;
                this.StrokeThickness = strokeThickness;
                this.TickFrequency = tickFrequency;
                this.Ticks = ticks;
                this.Padding = padding;
            }

            public TickBarPlacement Placement { get; }

            public bool IsDirectionReversed { get; }

            public double Value { get; }

            public double TickWidth { get; }

            public double StrokeThickness { get; }

            public double TickFrequency { get; }

            public DoubleCollection Ticks { get; }

            public Thickness Padding { get; }

            public static IEnumerable<TestCase> Create(
                double[] values,
                double[] tickWidths,
                double[] strokeThicknesses,
                double[] tickFrequencies,
                DoubleCollection[] tickCollections,
                Thickness[] paddings)
            {
                foreach (var placement in new[] { TickBarPlacement.Left, TickBarPlacement.Top, TickBarPlacement.Right, TickBarPlacement.Bottom })
                {
                    foreach (var isDirectionReversed in new[] { true, false })
                    {
                        foreach (var value in values)
                        {
                            foreach (var tickGap in tickWidths)
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
                                                    placement,
                                                    isDirectionReversed,
                                                    value,
                                                    tickGap,
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

            public override string ToString()
            {
                return $"{nameof(this.Placement)}: {this.Placement}, {nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.Value)}: {this.Value}, {nameof(this.TickWidth)}: {this.TickWidth}, {nameof(this.StrokeThickness)}: {this.StrokeThickness}, {nameof(this.TickFrequency)}: {this.TickFrequency}, {nameof(this.Ticks)}: {this.Ticks}, {nameof(this.Padding)}: {this.Padding}";
            }
        }
    }
}