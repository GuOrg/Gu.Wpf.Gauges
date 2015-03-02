namespace Gu.Gauges
{
    public interface IGauge
    {
        /// <summary>
        /// Gets or sets the current magnitude of the range control.  
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        double Value { get; set; }
        /// <summary>
        /// Gets the value with animated transitions.
        /// </summary>
        double AnimatedValue { get; }
    }
}