using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife.Components;

/// <summary>
/// A game component.
/// </summary>
public abstract class GameComponent
{
    /// <summary>
    /// Initialize the game component.
    /// </summary>
    public virtual void Initialize() { }

    /// <summary>
    /// Load content of the game component.
    /// </summary>
    public virtual void LoadContent(GraphicsDevice graphicsDevice) { }

    /// <summary>
    /// Update the game logic.
    /// </summary>
    /// <param name="gameTime">Provide a snapshot of timing values.</param>
    public virtual void Update(GameTime gameTime) { }

    /// <summary>
    /// Draw the component on the SpriteBatch.
    /// </summary>
    /// <param name="gameTime">Provide a snapshot of timing values.</param>
    /// <param name="spriteBatch">The sprite batch.</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
}
