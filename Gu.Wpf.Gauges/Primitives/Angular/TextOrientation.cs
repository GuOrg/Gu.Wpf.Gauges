namespace Gu.Wpf.Gauges
{
    /// <summary>
    /// Specifies how <see cref="AngularTextBar"/> draws text
    /// </summary>
    public enum TextOrientation
    {
        /// <summary>
        /// Draws text at 90° angle to line tick.
        /// </summary>
        Tangential,

        /// <summary>
        /// Draws text parallel to line tick, text starting from center.
        /// </summary>
        RadialOut,

        /// <summary>
        /// Draws text parallel to line tick, text starting from periphery.
        /// </summary>
        RadialIn,

        /// <summary>
        /// Use <see cref="TextTickBar.TextTransform"/>
        /// </summary>
        UseTransform
    }
}
