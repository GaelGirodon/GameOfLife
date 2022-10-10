namespace GameOfLife.Components;

/// <summary>
/// Game of Life cellular automaton initial state presets.
/// </summary>
public enum AutomatonPreset
{
    /// <summary>
    /// The "Blinker" oscillator (period 2).
    /// </summary>
    Blinker,

    /// <summary>
    /// A horizontal line of 5 cells evolving in 4 blinkers after a few cycles.
    /// </summary>
    FourBlinkers,

    /// <summary>
    /// A horizontal line of 4 cells evolving in a beehive (still life) after a few cycles.
    /// </summary>
    Beehive,

    /// <summary>
    /// The first-discovered pattern which evolve
    /// for long periods before stabilizing (Methuselahs).
    /// </summary>
    RPentomino,

    /// <summary>
    /// The smallest pattern with an infinite growth.
    /// </summary>
    SmallestInfiniteGrowth,

    /// <summary>
    /// The Gosper's glider gun, a pattern with an infinite growth.
    /// </summary>
    GosperGliderGun,

    /// <summary>
    /// A random pattern.
    /// </summary>
    Random
}
