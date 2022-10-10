using System;
using GameOfLife.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife;

/// <summary>
/// John Conway's Game of Life simple implementation.
/// </summary>
public class GameOfLifeGame : Game
{
    #region Constants

    /// <summary>
    /// Screen default size.
    /// </summary>
    private static Point ScreenSize => new Point(1600, 800);

    #endregion

    #region Fields

    /// <summary>
    /// Used to initialize and control the presentation of the graphics device.
    /// </summary>
    private GraphicsDeviceManager _graphics;

    /// <summary>
    /// Helper for drawing text strings and sprites in one or more optimized batches.
    /// </summary>
    private SpriteBatch _spriteBatch;

    /// <summary>
    /// Game of Life simulation.
    /// </summary>
    private readonly GameOfLifeAutomaton _automaton;

    #endregion

    #region Constructor

    /// <summary>
    /// Initialize the game.
    /// </summary>
    public GameOfLifeGame()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = ScreenSize.X,
            PreferredBackBufferHeight = ScreenSize.Y
        };
        Content.RootDirectory = "Content";
        // Create the cellular automaton
        _automaton = new GameOfLifeAutomaton(ScreenSize.Scale(0.5f), 220, 110, 8, Color.DodgerBlue,
            AutomatonPreset.GosperGliderGun, 100, 1500);
    }

    #endregion

    #region Game

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// Initialization logic.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
        // Initialize the simulation
        _automaton.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// Use this.Content to load the game content here.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // Load simulation content
        _automaton.LoadContent(GraphicsDevice);
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// Unload any non ContentManager content here.
    /// </summary>
    protected override void UnloadContent() { }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Increase or decrease the simulation speed
        if (Keyboard.GetState().IsAnyKeyDown(Keys.Add, Keys.Up)
            && _automaton.CycleDuration.TotalMilliseconds > 10)
            _automaton.CycleDuration -= TimeSpan.FromMilliseconds(1);
        else if (Keyboard.GetState().IsAnyKeyDown(Keys.Subtract, Keys.Down))
            _automaton.CycleDuration += TimeSpan.FromMilliseconds(1);

        // Update the simulation logic
        _automaton.Update(gameTime);

        base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // Draw simulation
        _spriteBatch.Begin();
        _automaton.Draw(gameTime, _spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    #endregion
}
