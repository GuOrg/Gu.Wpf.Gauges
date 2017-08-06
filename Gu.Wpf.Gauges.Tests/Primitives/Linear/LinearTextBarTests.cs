namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using NUnit.Framework;
    using Size = System.Windows.Size;

    [Apartment(ApartmentState.STA)]
    public class LinearTextBarTests
    {
        private static readonly IReadOnlyList<TestCase> RenderWithExplicitPositionCases = TestCase.Create(
            tickFrequencies: new[] { 0, 5.0 },
            tickCollections: new[] { null, (DoubleCollection)new DoubleCollection(new[] { 1.0, 2, 6 }).GetCurrentValueAsFrozen() },
            horizontalTextAlignments: Enum.GetValues(typeof(HorizontalTextAlignment)).Cast<HorizontalTextAlignment>(),
            verticalTextAlignments: Enum.GetValues(typeof(VerticalTextAlignment)).Cast<VerticalTextAlignment>(),
            paddings: new[] { default(Thickness) });

        private static readonly IReadOnlyList<TestCase> RenderWithDefaultPositionCases = TestCase.Create(
            tickFrequencies: new[] { 0, 5.0 },
            tickCollections: new[] { null, (DoubleCollection)new DoubleCollection(new[] { 1.0, 2, 6 }).GetCurrentValueAsFrozen() },
            paddings: new[] { default(Thickness) });

        [TestCaseSource(nameof(RenderWithExplicitPositionCases))]
        public void RenderWithExplicitPosition(TestCase testCase)
        {
            var tickBar = new LinearTextBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              TextPosition = new ExplicitLinearTextPosition(testCase.HorizontalTextAlignment, testCase.VerticalTextAlignment),
                              Placement = testCase.Placement,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                              FontFamily = new FontFamily("Arial"), // Seoge UI is measured differently on Win 7 and Win 10 for some reason
                              FontSize = 12,
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCaseSource(nameof(RenderWithDefaultPositionCases))]
        public void RenderWithDefaultPosition(TestCase testCase)
        {
            var tickBar = new LinearTextBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              TickFrequency = testCase.TickFrequency,
                              Ticks = testCase.Ticks,
                              Placement = testCase.Placement,
                              IsDirectionReversed = testCase.IsDirectionReversed,
                              Padding = testCase.Padding,
                              FontFamily = new FontFamily("Arial"), // Seoge UI is measured differently on Win 7 and Win 10 for some reason
                              FontSize = 12,
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Left, false, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Left, true, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Left, true, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Right, false, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Right, false, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Right, true, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Right, true, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Bottom, false, "0 0 0 0", "3,0,6,0")]
        [TestCase(TickBarPlacement.Bottom, false, "1 0 1 0", "2,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, true, "0 0 0 0", "6,0,3,0")]
        [TestCase(TickBarPlacement.Bottom, true, "1 0 1 0", "5,0,2,0")]
        [TestCase(TickBarPlacement.Top, false, "0 0 0 0", "3,0,6,0")]
        [TestCase(TickBarPlacement.Top, false, "1 0 1 0", "2,0,5,0")]
        [TestCase(TickBarPlacement.Top, true, "0 0 0 0", "6,0,3,0")]
        [TestCase(TickBarPlacement.Top, true, "1 0 1 0", "5,0,2,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, string padding, string expected)
        {
            var tickBar = new LinearTextBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
                FontFamily = new FontFamily("Arial"), // Seoge UI is measured differently on Win 7 and Win 10 for some reason
                FontSize = 12,
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

        private static string GetFileName(LinearTextBar tickBar)
        {
            if (tickBar.Ticks == null &&
                DoubleUtil.AreClose(tickBar.TickFrequency, 0))
            {
                return $"LinearTextBar_Placement_{tickBar.Placement}_Empty.png";
            }

            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var textPosition = tickBar.TextPosition is ExplicitLinearTextPosition explicitPos
                ? $"_Explicit_{explicitPos.Horizontal}_{explicitPos.Vertical}"
                : "_Default";

            return $@"LinearTextBar_Placement_{tickBar.Placement}_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_IsDirectionReversed_{tickBar.IsDirectionReversed}{textPosition}{tickFrequency}{ticks}{padding}.png"
                   .Replace(" ", "_");
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearTextBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\LinearTextBar");
            var size = tickBar.Placement.IsHorizontal()
                ? new Size(100, 15)
                : new Size(15, 100);
            tickBar.SaveImage(size, $@"C:\Temp\LinearTextBar\{GetFileName(tickBar)}");
        }

        public class TestCase
        {
            public TestCase(
                TickBarPlacement placement,
                bool isDirectionReversed,
                HorizontalTextAlignment horizontalTextAlignment,
                VerticalTextAlignment verticalTextAlignment,
                double tickFrequency,
                DoubleCollection ticks,
                Thickness padding)
            {
                this.Placement = placement;
                this.IsDirectionReversed = isDirectionReversed;
                this.TickFrequency = tickFrequency;
                this.Ticks = ticks;
                this.Padding = padding;
                this.HorizontalTextAlignment = horizontalTextAlignment;
                this.VerticalTextAlignment = verticalTextAlignment;
            }

            public TickBarPlacement Placement { get; }

            public bool IsDirectionReversed { get; }

            public double TickFrequency { get; }

            public DoubleCollection Ticks { get; }

            public HorizontalTextAlignment HorizontalTextAlignment { get; }

            public VerticalTextAlignment VerticalTextAlignment { get; }

            public Thickness Padding { get; }

            public static IReadOnlyList<TestCase> Create(
                double[] tickFrequencies,
                DoubleCollection[] tickCollections,
                IEnumerable<HorizontalTextAlignment> horizontalTextAlignments,
                IEnumerable<VerticalTextAlignment> verticalTextAlignments,
                Thickness[] paddings)
            {
                var testCases = new List<TestCase>();
                foreach (var placement in new[] { TickBarPlacement.Left, TickBarPlacement.Top, TickBarPlacement.Right, TickBarPlacement.Bottom })
                {
                    foreach (var isDirectionReversed in new[] { true, false })
                    {
                        foreach (var horizontalTextAlignment in horizontalTextAlignments)
                        {
                            foreach (var verticalTextAlignment in verticalTextAlignments)
                            {
                                {
                                    foreach (var tickFrequency in tickFrequencies)
                                    {
                                        foreach (var ticks in tickCollections)
                                        {
                                            foreach (var padding in paddings)
                                            {
                                                testCases.Add(new TestCase(
                                                                  placement: placement,
                                                                  isDirectionReversed: isDirectionReversed,
                                                                  horizontalTextAlignment: horizontalTextAlignment,
                                                                  verticalTextAlignment: verticalTextAlignment,
                                                                  tickFrequency: tickFrequency,
                                                                  ticks: ticks,
                                                                  padding: padding));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return testCases;
            }

            public static IReadOnlyList<TestCase> Create(
                double[] tickFrequencies,
                DoubleCollection[] tickCollections,
                Thickness[] paddings)
            {
                var testCases = new List<TestCase>();
                foreach (var placement in new[] { TickBarPlacement.Left, TickBarPlacement.Top, TickBarPlacement.Right, TickBarPlacement.Bottom })
                {
                    foreach (var isDirectionReversed in new[] { true, false })
                    {
                        foreach (var tickFrequency in tickFrequencies)
                        {
                            foreach (var ticks in tickCollections)
                            {
                                foreach (var padding in paddings)
                                {
                                    testCases.Add(
                                        new TestCase(
                                            placement: placement,
                                            isDirectionReversed: isDirectionReversed,
                                            horizontalTextAlignment: default(HorizontalTextAlignment),
                                            verticalTextAlignment: default(VerticalTextAlignment),
                                            tickFrequency: tickFrequency,
                                            ticks: ticks,
                                            padding: padding));
                                }
                            }
                        }
                    }
                }

                return testCases;
            }

            public override string ToString()
            {
                return $"{nameof(this.Placement)}: {this.Placement}, {nameof(this.IsDirectionReversed)}: {this.IsDirectionReversed}, {nameof(this.TickFrequency)}: {this.TickFrequency}, {nameof(this.Ticks)}: {this.Ticks}, {nameof(this.HorizontalTextAlignment)}: {this.HorizontalTextAlignment}, {nameof(this.VerticalTextAlignment)}: {this.VerticalTextAlignment}, {nameof(this.Padding)}: {this.Padding}";
            }
        }
    }
}