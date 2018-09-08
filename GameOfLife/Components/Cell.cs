using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Components
{
    /// <summary>
    /// A cell of the Game of Life.
    /// </summary>
    public class Cell : GameComponent
    {
        #region Fields

        /// <summary>
        /// Position on the game viewport.
        /// </summary>
        private readonly Point _position;

        /// <summary>
        /// Cell size in pixels.
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Cell fill color.
        /// </summary>
        private readonly Color _color;

        /// <summary>
        /// Cell texture.
        /// </summary>
        private readonly Texture2D _texture;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a cell of the Game of Life simulation.
        /// </summary>
        /// <param name="position">Cell position on the game viewport.</param>
        /// <param name="size">Cell size in pixels.</param>
        /// <param name="color">Cell fill color.</param>
        /// <param name="texture">Cell texture.</param>
        public Cell(Point position, int size, Color color, Texture2D texture) {
            _size = size;
            _color = color;
            _texture = texture;
            _position = position;
        }

        #endregion

        #region Game

        /// <inheritdoc />
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, new Rectangle(_position.X, _position.Y, _size + 1, _size + 1), _color);
        }

        #endregion
    }
}