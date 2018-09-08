using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Components
{
    /// <summary>
    /// A grid.
    /// </summary>
    public class Grid : GameComponent
    {
        #region Fields

        /// <summary>
        /// Grid size in pixels.
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Grid columns count.
        /// </summary>
        private readonly int _cols;

        /// <summary>
        /// Grid rows count.
        /// </summary>
        private readonly int _rows;

        /// <summary>
        /// Position of the center of the grid on the viewport.
        /// </summary>
        private readonly Point _center;

        /// <summary>
        /// Grid color.
        /// </summary>
        private readonly Color _color;

        /// <summary>
        /// Grid pixel texture.
        /// </summary>
        private Texture2D _texture;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a grid.
        /// </summary>
        /// <param name="size">Space between columns/rows in pixels.</param>
        /// <param name="cols">Number of columns (-1 => fill the game viewport).</param>
        /// <param name="rows">Number of rows (-1 => fill the game viewport).</param>
        /// <param name="center">Center of the grid on the viewport.</param>
        /// <param name="color">Lines color.</param>
        public Grid(int size, int cols, int rows, Point center, Color color) {
            _size = size;
            _cols = cols;
            _rows = rows;
            _center = center;
            _color = color;
        }

        #endregion

        #region Game

        /// <inheritdoc />
        public override void LoadContent(GraphicsDevice graphicsDevice) {
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new[] {_color});
        }

        /// <inheritdoc />
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // View port size
            var width = _cols > 0 ? _cols * _size : spriteBatch.GraphicsDevice.Viewport.Width;
            var height = _rows > 0 ? _rows * _size : spriteBatch.GraphicsDevice.Viewport.Height;
            // Number of columns and rows
            var cols = width / _size;
            var rows = height / _size;
            // Draw columns
            var colY = _center.Y - height / 2;
            for (var x = -cols / 2; x <= cols / 2; x++)
                spriteBatch.Draw(_texture, new Rectangle(_center.X + x * _size, colY, 1, height), _color);
            // Draw rows
            var rowX = _center.X - width / 2;
            for (var y = -rows / 2; y <= rows / 2; y++) {
                spriteBatch.Draw(_texture, new Rectangle(rowX, _center.Y + y * _size, width, 1), _color);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Compute the real position of a point (on the game viewport)
        /// based on its virtual coordinates on the grid (orthonormal coordinate system).
        /// </summary>
        /// <param name="coords">Coordinates on the grid.</param>
        /// <returns>Position on the game viewport.</returns>
        public Point ComputePosition(Point coords) {
            return new Point(_center.X + coords.X * _size, _center.Y - _size - coords.Y * _size);
        }

        #endregion
    }
}