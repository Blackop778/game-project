using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameProject.Engine;
using GameProject.Actors;

namespace GameProject
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static Game Instance { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }
        public int DisplayWidth => _graphics.PreferredBackBufferWidth;
        public int DisplayHeight => _graphics.PreferredBackBufferHeight;
        public SpriteFont DebugFont { get; private set; }
        public Texture2D DebugPixel { get; private set; }

        private GraphicsDeviceManager _graphics;
        private List<Actor> actors;
        private List<Actor> toDestroy;
        private bool contentLoaded = false;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            //Window.IsBorderless = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
        }

        protected override void Initialize()
        {
            actors = new List<Actor>();
            toDestroy = new List<Actor>();

            Instantiate(new Player());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            DebugFont = Content.Load<SpriteFont>("debug");
            DebugPixel = Content.Load<Texture2D>("1x1WhitePixel");

            foreach (Actor actor in actors)
                actor.LoadContent(Content);

            contentLoaded = true;
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            if (InputManager.Exit)
                Exit();

            for (int i = 0; i < actors.Count; i++)
            {
                Actor actor = actors[i];
                if (!actor.IsDestroyed)
                    actor.EngineUpdate(gameTime);
            }

            while (toDestroy.Count > 0)
            {
                actors.Remove(toDestroy[0]);
                toDestroy.RemoveAt(0);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.YellowGreen);

            SpriteBatch.Begin();
            foreach (Actor actor in actors)
            {
                if (!actor.IsDestroyed)
                    actor.Draw(gameTime);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public Actor Instantiate(Actor a)
        {
            actors.Add(a);

            a.Start();

            if (contentLoaded)
                a.LoadContent(Content);

            return a;
        }

        public void Destroy(Actor a)
        {
            toDestroy.Add(a);
            if (!a.IsDestroyed)
                a.Destroy(a);
        }
    }
}
