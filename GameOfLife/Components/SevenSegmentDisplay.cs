using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Components
{
    /// <summary>
    /// A seven-segment display built to display positive integers.
    /// </summary>
    public class SevenSegmentDisplay : GameComponent
    {
        #region Properties

        /// <summary>
        /// Value to display.
        /// </summary>
        public int Value { get; set; }

        #endregion

        #region Fields

        /// <summary>
        /// Display position (top-left corner position).
        /// </summary>
        private readonly Point _position;

        /// <summary>
        /// Character width in pixels.
        /// </summary>
        private readonly int _charWidth;

        /// <summary>
        /// Character height in pixels.
        /// </summary>
        private readonly int _charHeight;

        /// <summary>
        /// Segment width in pixels.
        /// </summary>
        private readonly int _thickness;

        /// <summary>
        /// Segment color.
        /// </summary>
        private readonly Color _color;

        /// <summary>
        /// Space between characters.
        /// </summary>
        private readonly int _space;

        /// <summary>
        /// Segment texture.
        /// </summary>
        private Texture2D _texture;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a seven-segment display to display positive integers.
        /// </summary>
        /// <param name="position">Display position (top-left corner position).</param>
        /// <param name="charWidth">Character width in pixels.</param>
        /// <param name="charHeight">Character height in pixels.</param>
        /// <param name="thickness">Segment width in pixels.</param>
        /// <param name="color">Segment color.</param>
        /// <param name="space">Space between characters.</param>
        public SevenSegmentDisplay(Point position, int charWidth, int charHeight, int thickness, Color color,
            int space) {
            _position = position;
            _charWidth = charWidth;
            _charHeight = charHeight;
            _color = color;
            _thickness = thickness;
            _space = space;
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
            if (Value < 0) return;
            // Get digits
            var digits = Value.Digits();
            var origin = _position;
            // Draw each digit
            foreach (var digit in digits) {
                // Segments definition
                var segments = Chars[digit];
                // Segment A
                if (segments[0])
                    spriteBatch.Draw(_texture, new Rectangle(origin.X, origin.Y, _charWidth, _thickness), _color);
                // Segment B
                if (segments[1])
                    spriteBatch.Draw(_texture,
                        new Rectangle(origin.X + _charWidth - _thickness, origin.Y, _thickness, _charHeight / 2),
                        _color);
                // Segment C
                if (segments[2])
                    spriteBatch.Draw(_texture,
                        new Rectangle(origin.X + _charWidth - _thickness, origin.Y + _charHeight / 2, _thickness,
                            _charHeight / 2), _color);
                // Segment D
                if (segments[3])
                    spriteBatch.Draw(_texture,
                        new Rectangle(origin.X, origin.Y + _charHeight - _thickness, _charWidth, _thickness), _color);
                // Segment E
                if (segments[4])
                    spriteBatch.Draw(_texture,
                        new Rectangle(origin.X, origin.Y + _charHeight / 2, _thickness, _charHeight / 2), _color);
                // Segment F
                if (segments[5])
                    spriteBatch.Draw(_texture, new Rectangle(origin.X, origin.Y, _thickness, _charHeight / 2), _color);
                // Segment G
                if (segments[6])
                    spriteBatch.Draw(_texture,
                        new Rectangle(origin.X, origin.Y + _charHeight / 2 - _thickness / 2, _charWidth, _thickness),
                        _color);
                // Shift to the next character
                origin += new Point(_charWidth + _space, 0);
            }
        }

        #endregion

        #region Characters

        /// <summary>
        /// Characters segments definition.
        /// Each digit is associated to a list of seven booleans representing
        /// the seven segments of the display (a, b, c, d, e, f, g).
        /// </summary>
        private static readonly Dictionary<byte, bool[]> Chars = new Dictionary<byte, bool[]> {
            {0, new[] {true, true, true, true, true, true, false}},
            {1, new[] {false, true, true, false, false, false, false}},
            {2, new[] {true, true, false, true, true, false, true}},
            {3, new[] {true, true, true, true, false, false, true}},
            {4, new[] {false, true, true, false, false, true, true}},
            {5, new[] {true, false, true, true, false, true, true}},
            {6, new[] {true, false, true, true, true, true, true}},
            {7, new[] {true, true, true, false, false, false, false}},
            {8, new[] {true, true, true, true, true, true, true}},
            {9, new[] {true, true, true, true, false, true, true}}
        };

        #endregion
    }
}