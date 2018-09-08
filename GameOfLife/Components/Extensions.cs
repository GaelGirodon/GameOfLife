using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameOfLife.Components
{
    /// <summary>
    /// Extensions utilities.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Multiply point coordinates by a ratio.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <param name="ratio">The multiplication ratio.</param>
        /// <returns>The computed point.</returns>
        public static Point Scale(this Point p, float ratio)
            => new Point((int) (p.X * ratio), (int) (p.Y * ratio));
        
        /// <summary>
        /// Get a list of the digits of a number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The digits as a list of numbers.</returns>
        public static IEnumerable<byte> Digits(this int value) {
            return Math.Abs(value).ToString().ToCharArray().ToList().Select(d => (byte) (d - '0'));
        }
    }
}