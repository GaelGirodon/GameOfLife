using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife;

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
    {
        return new Point((int)(p.X * ratio), (int)(p.Y * ratio));
    }

    /// <summary>
    /// Get a list of the digits of a number.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns>The digits as a list of numbers.</returns>
    public static IEnumerable<byte> Digits(this int value)
    {
        return Math.Abs(value).ToString().ToCharArray().ToList().Select(d => (byte)(d - '0'));
    }

    /// <summary>
    /// Gets whether at least one of the given keys
    /// is currently being pressed.
    /// </summary>
    /// <param name="state">The keyboard state.</param>
    /// <param name="keys">The keys to check.</param>
    /// <returns>true if one of the given keys is currently being pressed, false elsewhere.</returns>
    public static bool IsAnyKeyDown(this KeyboardState state, params Keys[] keys)
    {
        return keys.ToList().Any(state.IsKeyDown);
    }
}
