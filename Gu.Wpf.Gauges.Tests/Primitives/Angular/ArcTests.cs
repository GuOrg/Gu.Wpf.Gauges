namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class ArcTests
    {
        private static readonly IReadOnlyList<TestCase> RenderCases = TestCase.Create(
            starts: new[] { -90.0, -140.0 },
            ends: new[] { 90.0, 140.0 },
            thicknesses: new[] { 0, 10, double.PositiveInfinity },
            values: new[] { double.NaN, 0, 5, 10 },
            strokeThicknesses: new[] { 0.0, 1 },
            paddings: new[] { default(Thickness) })
            .Where(x => !(DoubleUtil.IsZero(x.StrokeThickness) && DoubleUtil.IsZero(x.Thickness)))
            .ToArray();

        [TestCaseSource(nameof(RenderCases))]
        public void RenderNoStroke(TestCase testCase)
        {
            var arc = new Arc
            {
                Minimum = 0,
                Maximum = 10,
                Fill = Brushes.Red,
                Start = testCase.Start,
                End = testCase.End,
                IsDirectionReversed = testCase.IsDirectionReversed,
                Thickness = testCase.Thickness,
                Stroke = Brushes.Black,
                StrokeThickness = testCase.StrokeThickness,
                StrokeStartLineCap = testCase.StrokeStartLineCap,
                StrokeEndLineCap = testCase.StrokeEndLineCap,
                StrokeDashCap = testCase.StrokeDashCap,
                StrokeDashArray = testCase.StrokeDashArray,
                Value = testCase.Value,
                Padding = testCase.Padding
            };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(true, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 2, "0 0 0 1", "0,0,0,0")]
        [TestCase(true, 4, 2, "0 0 0 1", "0,0,0,0")]
        public void Overflow(bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new Arc
            {
                StrokeThickness = strokeThickness,
                Start = -140,
                End = 140,
                Minimum = 0,
                Maximum = 10,
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

        private static string GetFileName(Arc arc)
        {
            var thickness = double.IsInfinity(arc.Thickness) ? "inf" : arc.Thickness.ToString(CultureInfo.InvariantCulture);
            if (double.IsNaN(arc.Value) ||
                DoubleUtil.AreClose(arc.Value, arc.Maximum))
            {
                return $"Arc_Start_{arc.Start}_End_{arc.End}_Thickness_{thickness}_StrokeThickness_{arc.StrokeThickness}.png";
            }

            return DoubleUtil.IsZero(arc.Value)
                ? $"Arc_Value_0.png"
                : $"Arc_Value_{arc.Value}_Min_{arc.Minimum}_Max_{arc.Maximum}_IsDirectionReversed_{arc.IsDirectionReversed}_Start_{arc.Start}_End_{arc.End}_Thickness_{thickness}_StrokeThickness_{arc.StrokeThickness}.png";
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(Arc arc)
        {
            var directory = Directory.CreateDirectory($@"C:\Temp\Arc");
            arc.SaveImage(
                new Size(30, 30),
                Path.Combine(directory.FullName, GetFileName(arc)));
        }

        public class TestCase
        {
            public TestCase(
                double start,
                double end,
                bool isDirectionReversed,
                double thickness,
                double value,
                double strokeThickness,
                Thickness padding)
            {
                this.Start = start;
                this.End = end;
                this.IsDirectionReversed = isDirectionReversed;
                this.Thickness = thickness;
                this.Value = value;
                this.StrokeThickness = strokeThickness;
                this.Padding = padding;
            }

            public double Start { get; }

            public double End { get; }

            public bool IsDirectionReversed { get; }

            public double Thickness { get; }

            public double Value { get; }

            public double StrokeThickness { get; }

            public Thickness Padding { get; }

            public PenLineCap StrokeStartLineCap => this.StrokeThickness >= this.Thickness
                ? PenLineCap.Round
                : PenLineCap.Flat;

            public PenLineCap StrokeEndLineCap => this.StrokeThickness >= this.Thickness
                ? PenLineCap.Round
                : PenLineCap.Flat;

            public PenLineCap StrokeDashCap => this.StrokeThickness >= this.Thickness
                ? PenLineCap.Round
                : PenLineCap.Flat;

            public DoubleCollection StrokeDashArray => this.StrokeThickness >= this.Thickness
                ? new DoubleCollection(new[] { 0.0, 1.0 })
                : null;

            public static IEnumerable<TestCase> Create(
                double[] starts,
                double[] ends,
                double[] thicknesses,
                double[] values,
                double[] strokeThicknesses,
                Thickness[] paddings)
            {
                foreach (var start in starts)
                {
                    foreach (var end in ends)
                    {
                        foreach (var isDirectionReversed in new[] { true, false })
                        {
                            foreach (var thickness in thicknesses)
                            {
                                foreach (var value in values)
                                {
                                    foreach (var strokeThickness in strokeThicknesses)
                                    {
                                        foreach (var padding in paddings)
                                        {
                                            yield return new TestCase(
                                                start: start,
                                                end: end,
                                                isDirectionReversed: isDirectionReversed,
                                                thickness: thickness,
                                                value: value,
                                                strokeThickness: strokeThickness,
                                                padding: padding);
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
                return $"{nameof(this.Start)}: {this.Start}, {nameof(this.End)}: {this.End}, {nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.Thickness)}: {this.Thickness}, {nameof(this.Value)}: {this.Value}, {nameof(this.StrokeThickness)}: {this.StrokeThickness}, {nameof(this.Padding)}: {this.Padding}, {nameof(this.StrokeStartLineCap)}: {this.StrokeStartLineCap}, {nameof(this.StrokeEndLineCap)}: {this.StrokeEndLineCap}, {nameof(this.StrokeDashCap)}: {this.StrokeDashCap}, {nameof(this.StrokeDashArray)}: {this.StrokeDashArray}";
            }
        }
    }
}