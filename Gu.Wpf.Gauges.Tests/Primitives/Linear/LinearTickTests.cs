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
    public class LinearTickTests
    {
        private static readonly IReadOnlyList<TestCase> RenderCases = TestCase.Create(
                                                                                  values: new[] { double.NaN, 0, 5, 10 },
                                                                                  tickWidths: new[] { 1.0, 5 },
                                                                                  strokeThicknesses: new[] { 0.0, 1 },
                                                                                  paddings: new[] { default(Thickness) })
                                                                              .ToArray();

        [TestCaseSource(nameof(RenderCases))]
        public void Render(TestCase testCase)
        {
            var tickBar = new LinearTick
                          {
                              StrokeThickness = testCase.StrokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Value = testCase.Value,
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
            var tickBar = new LinearTick
                          {
                              StrokeThickness = strokeThickness,
                              Minimum = 0,
                              Maximum = 10,
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

        private static string GetFileName(LinearTick tickBar)
        {
            var orientation = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            if (double.IsNaN(tickBar.Value))
            {
                return $"LinearTick_Value_NaN{orientation}.png";
            }

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            return $@"LinearTick_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_Value_{tickBar.Value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}{orientation}.png"
                .Replace(" ", "_");
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearTick tickBar)
        {
            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(10, 100)
                : new Size(100, 10);
            Directory.CreateDirectory(@"C:\Temp\LinearTick");
            tickBar.SaveImage(size, $@"C:\Temp\LinearTick\{GetFileName(tickBar)}");
        }

        public class TestCase
        {
            public TestCase(
                TickBarPlacement placement,
                bool isDirectionReversed,
                double value,
                double tickWidth,
                double strokeThickness,
                Thickness padding)
            {
                this.Placement = placement;
                this.IsDirectionReversed = isDirectionReversed;
                this.Value = value;
                this.TickWidth = tickWidth;
                this.StrokeThickness = strokeThickness;
                this.Padding = padding;
            }

            public TickBarPlacement Placement { get; }

            public bool IsDirectionReversed { get; }

            public double Value { get; }

            public double TickWidth { get; }

            public double StrokeThickness { get; }

            public Thickness Padding { get; }

            public static IEnumerable<TestCase> Create(
                double[] values,
                double[] tickWidths,
                double[] strokeThicknesses,
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
                                    foreach (var padding in paddings)
                                    {
                                        yield return new TestCase(
                                            placement,
                                            isDirectionReversed,
                                            value,
                                            tickGap,
                                            strokeThickness,
                                            padding);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                return $"{nameof(this.Placement)}: {this.Placement}, {nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.Value)}: {this.Value}, {nameof(this.TickWidth)}: {this.TickWidth}, {nameof(this.StrokeThickness)}: {this.StrokeThickness}, {nameof(this.Padding)}: {this.Padding}";
            }
        }
    }
}