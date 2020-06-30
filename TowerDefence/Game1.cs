using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using TowerDefence.Managers;
using TowerDefence.Screens;

namespace TowerDefence
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Point ScreenSize { get; private set; }
        public static ContentManager ContentManager;
        public static GraphicsDevice Graphics;

        public float GameSpeed { get; set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SimpleFps fpsCounter;
        private DateTime time;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 100d);
            graphics.PreferredBackBufferWidth  = 1000;
            graphics.PreferredBackBufferHeight = 550;

            ScreenSize = new Point(1000, 550);
            time = DateTime.Now;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameSpeed = 1f;
            Graphics = GraphicsDevice;
            ContentManager = Content;

            fpsCounter = new SimpleFps();
            ScreenManager.ChangeScreen(new LoadScreen());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            DateTime now = DateTime.Now;

            float elapsed = (float)(now - time).TotalSeconds;
            time = now;
            if (Keyboard.GetState().IsKeyDown(Keys.F11)) Exit();

            fpsCounter.Update(elapsed);
            Window.Title = fpsCounter.msg;

            ScreenManager.Update(elapsed * GameSpeed);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ScreenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
