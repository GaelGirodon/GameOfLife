using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Components
{
    /// <summary>
    /// Game of Life cellular automaton.
    /// </summary>
    public class GameOfLifeAutomaton : GameComponent
    {
        #region Fields

        /// <summary>
        /// Simulation width (number of cells on the X axis).
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// Simulation height (number of cells on the Y axis).
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// Simulation initial state.
        /// </summary>
        private readonly AutomatonPreset _preset;

        /// <summary>
        /// Simulation grid.
        /// </summary>
        private readonly Grid _grid;

        /// <summary>
        /// Simulation cells.
        /// </summary>
        private readonly Cell[,] _cells;

        /// <summary>
        /// Size of a cell in pixels.
        /// </summary>
        private readonly int _cellSize;

        /// <summary>
        /// The cell color.
        /// </summary>
        private readonly Color _cellColor;

        /// <summary>
        /// The cell texture.
        /// </summary>
        private Texture2D _cellTexture;

        /// <summary>
        /// Simulation cycle duration (milliseconds).
        /// </summary>
        public TimeSpan CycleDuration { get; set; }

        /// <summary>
        /// Initial delay before simulation start (milliseconds).
        /// </summary>
        private readonly TimeSpan _initialDelay;

        /// <summary>
        /// Number of computed simulation cycles.
        /// </summary>
        private int _cycleCount;

        /// <summary>
        /// Cycle count display.
        /// </summary>
        private readonly SevenSegmentDisplay _display;

        /// <summary>
        /// Elapsed time since the last simulation cycle.
        /// </summary>
        private TimeSpan _elapsedTimeSinceLastCycle;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the simulation.
        /// </summary>
        /// <param name="center">Simulation center position.</param>
        /// <param name="width">Simulation width (number of cells on the X axis).</param>
        /// <param name="height">Simulation height (number of cells on the Y axis).</param>
        /// <param name="cellSize">Size of a cell in pixels.</param>
        /// <param name="cellColor">Color of a cell.</param>
        /// <param name="preset">Simulation initial state.</param>
        /// <param name="cycleDuration">Simulation cycle duration (milliseconds).</param>
        /// <param name="initialDelay">Initial delay before simulation start (milliseconds).</param>
        public GameOfLifeAutomaton(Point center, int width, int height, int cellSize,
            Color cellColor, AutomatonPreset preset, int cycleDuration, int initialDelay) {
            // Basic fields
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _cellColor = cellColor;
            _preset = preset;
            // Grid
            _grid = new Grid(cellSize, width, height, center, new Color(245, 245, 245));
            // Cells
            _cells = new Cell[height, width];
            // Simulation variables
            _initialDelay = TimeSpan.FromMilliseconds(initialDelay);
            CycleDuration = TimeSpan.FromMilliseconds(cycleDuration);
            _cycleCount = 0;
            _elapsedTimeSinceLastCycle = TimeSpan.Zero;
            // Seven-segment display
            _display = new SevenSegmentDisplay(new Point(32, 32), cellSize * 4, cellSize * 7,
                cellSize, Color.Red, cellSize);
        }

        #endregion

        #region Game

        /// <inheritdoc />
        public override void Initialize() {
            // Alive cells on the first cycle
            switch (_preset) {
                case AutomatonPreset.Blinker:
                    CreateCellsFromCoords(new[,] {{-1, 0}, {0, 0}, {1, 0}});
                    break;
                case AutomatonPreset.FourBlinkers:
                    CreateCellsFromCoords(new[,] {{-2, 0}, {-1, 0}, {0, 0}, {1, 0}, {2, 0}});
                    break;
                case AutomatonPreset.Beehive:
                    CreateCellsFromCoords(new[,] {{-2, 0}, {-1, 0}, {0, 0}, {1, 0}});
                    break;
                case AutomatonPreset.RPentomino:
                    CreateCellsFromCoords(new[,] {{0, 0}, {-1, 0}, {0, -1}, {0, 1}, {1, 1}});
                    break;
                case AutomatonPreset.SmallestInfiniteGrowth:
                    CreateCellsFromCoords(new[,] {
                        {-10, 10}, {-8, 10}, {-8, 9}, {-6, 8}, {-6, 7}, {-6, 6},
                        {-4, 7}, {-4, 6}, {-4, 5}, {-3, 6}
                    });
                    break;
                case AutomatonPreset.GosperGliderGun:
                    CreateCellsFromCoords(new[,] {
                        {-17, 0}, {-17, -1}, {-16, 0}, {-16, -1},
                        {-4, 2}, {-5, 2}, {-6, 1}, {-7, 0}, {-7, -1}, {-7, -2}, {-6, -3}, {-5, -4}, {-4, -4},
                        {-3, -1}, {-2, 1}, {-1, 0}, {-1, -1}, {-1, -2}, {-2, -3}, {0, -1}, {-3, -1},
                        {7, 4}, {7, 3}, {5, 3}, {4, 2}, {4, 1}, {4, 0}, {3, 2}, {3, 1}, {3, 0}, {5, -1}, {7, -1},
                        {7, -2},
                        {17, 2}, {17, 1}, {18, 2}, {18, 1}
                    });
                    break;
                case AutomatonPreset.Random:
                    var rand = new Random();
                    ForEachCell((cell, y, x) => {
                        if (rand.NextDouble() >= 0.75) {
                            CreateCell(x, y);
                        }
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public override void LoadContent(GraphicsDevice graphicsDevice) {
            // Load grid content
            _grid.LoadContent(graphicsDevice);
            // Load seven-segment display content
            _display.LoadContent(graphicsDevice);
            // Cell global texture
            _cellTexture = new Texture2D(graphicsDevice, 1, 1);
            _cellTexture.SetData(new[] {_cellColor});
        }

        /// <inheritdoc />
        public override void Update(GameTime gameTime) {
            // Initial delay before simulation start
            if (gameTime.TotalGameTime < _initialDelay) {
                return;
            }

            // else: delay between cycles
            _elapsedTimeSinceLastCycle += gameTime.ElapsedGameTime;
            if (_elapsedTimeSinceLastCycle < CycleDuration) {
                return;
            }

            // else: simulation cycle
            _elapsedTimeSinceLastCycle = TimeSpan.FromTicks(
                _elapsedTimeSinceLastCycle.Ticks % CycleDuration.Ticks);
            _display.Value = ++_cycleCount;

            // Run simulation
            var deadCells = new List<Point>();
            var newCells = new List<Point>();
            ForEachCell((cell, y, x) => {
                // Count current cell alive neighbours
                var aliveNeighbours = 0;
                for (var nY = Math.Max(0, y - 1); nY <= Math.Min(_cells.GetLength(0) - 1, y + 1); nY++) {
                    for (var nX = Math.Max(0, x - 1); nX <= Math.Min(_cells.GetLength(1) - 1, x + 1); nX++) {
                        if ((nX != x || nY != y) && _cells[nY, nX] != null) {
                            aliveNeighbours++;
                        }
                    }
                }

                // Game of Life algorithm
                if (cell == null && aliveNeighbours == 3) {
                    // Dead cell with 3 alive neighbours => alive
                    newCells.Add(new Point(x, y));
                } else if (cell != null && aliveNeighbours != 2 && aliveNeighbours != 3) {
                    // Alive cell with 2 or 3 alive neighbours => stay alive, else => dead
                    deadCells.Add(new Point(x, y));
                }
            });

            // Update cells
            deadCells.ForEach(c => _cells[c.Y, c.X] = null);
            newCells.ForEach(c => CreateCell(c.X, c.Y));
        }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // Draw the grid
            _grid.Draw(gameTime, spriteBatch);

            // Draw the cells
            ForEachCell((c, y, x) => c?.Draw(gameTime, spriteBatch));

            // Draw the seven-segment display
            _display.Draw(gameTime, spriteBatch);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Execute an action for each cell in the matrix.
        /// </summary>
        /// <param name="action">The action to execute (arguments: cell, y, x).</param>
        private void ForEachCell(Action<Cell, int, int> action) {
            for (var y = 0; y < _cells.GetLength(0); y++) {
                for (var x = 0; x < _cells.GetLength(1); x++) {
                    action.Invoke(_cells[y, x], y, x);
                }
            }
        }

        /// <summary>
        /// Create a cell in the array at the given index.
        /// </summary>
        /// <param name="x">X index (second array dimension).</param>
        /// <param name="y">Y index (first array dimension).</param>
        private void CreateCell(int x, int y) {
            _cells[y, x] = new Cell(_grid.ComputePosition(new Point(x - _width / 2, y - _height / 2)),
                _cellSize, _cellColor, _cellTexture);
        }

        /// <summary>
        /// Get cell index in the array from grid coordinates.
        /// </summary>
        /// <param name="x">X coordinate on the grid.</param>
        /// <param name="y">Y coordinate on the grid.</param>
        /// <returns>The cell index in the array.</returns>
        private Point GetCellIndexFromCoords(int x, int y) {
            return new Point(x + _cells.GetLength(1) / 2, y + _cells.GetLength(0) / 2);
        }

        /// <summary>
        /// Create multiple cells from grid coordinates.
        /// </summary>
        /// <param name="coords">Cells coordinates on the grid.</param>
        private void CreateCellsFromCoords(int[,] coords) {
            for (var i = 0; i < coords.GetLength(0); i++) {
                var c = GetCellIndexFromCoords(coords[i, 0], coords[i, 1]);
                CreateCell(c.X, c.Y);
            }
        }

        #endregion
    }
}