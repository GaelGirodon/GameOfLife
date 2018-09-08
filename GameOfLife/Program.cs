using System;

namespace GameOfLife
{
    /// <summary>
    /// The main program class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Create a new Game of Life instance and run it.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = new GameOfLifeGame())
                game.Run();
        }
    }
}