namespace Gu.Wpf.Gauges
{
    /// <summary>
    /// Specifies where ticks are placed.
    /// </summary>
    public enum TickSnap
    {
        /// <summary>
        /// This setting places the first tick at Minimum % TickFrequency
        /// </summary>
        TickFrequency,

        /// <summary>
        /// This setting starts with minimum and increments TickFrequency.
        /// </summary>
        Minimum,

        /// <summary>
        /// This setting ends at maximum and increments TickFrequency.
        /// </summary>
        Maximum,
    }
}