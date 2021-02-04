using GameProject.Components;
using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    internal class Player : Actor
    {
        private Texture2D sprite;
        private Rigidbody2D rigidbody;

        public Player(Game game) : base(game)
        {

        }

        internal override void Start()
        {
            base.Start();

            rigidbody = AddComponent(new Rigidbody2D(this)) as Rigidbody2D;
            rigidbody.MaxVelocity = 400;
            rigidbody.Drag = 100f;

            AddComponent(new PlayerController(this));
        }

        internal override void Update(GameTime gameTime)
        {
            
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("Oval");
        }

        internal override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            RenderSprite(Position, sprite);
            RenderDebugText(new Vector2(20, 20), rigidbody.Velocity.ToString());
            RenderDebugText(new Vector2(20, 40), rigidbody.Acceleration.ToString());
        }
    }
}
