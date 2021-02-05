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
    class Boomerang : Actor
    {
        private Vector2 destination;
        private readonly Actor user;
        private bool returning;
        private Rigidbody2D rigidbody;
        private Texture2D sprite;
        private readonly float maxVelocity = 400;
        private readonly float minVelocity = 200;
        private readonly float maximumInteractRange = 50;

        public Boomerang(Vector2 position, Vector2 destination, Actor user) : base(position) {
            this.destination = destination;
            this.user = user;
        }

        internal override void Start()
        {
            base.Start();

            rigidbody = AddComponent(new Rigidbody2D(this)) as Rigidbody2D;
            rigidbody.MaxVelocity = maxVelocity;
            rigidbody.Drag = 0.25f;
            returning = false;

            //SetVelocity(1);
        }

        internal override void Update(GameTime gameTime)
        {
            if (returning)
                destination = user.Position;

            if (Vector2.Distance(Position, destination) < maximumInteractRange)
            {
                if (!returning)
                    returning = true;
                else
                    Destroy(this);
            }

            //if (returning)
                SetVelocity((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void SetVelocity(float elapsedSeconds)
        {
            Vector2 instantVel = destination - Position;

            if (instantVel.LengthSquared() < (minVelocity * minVelocity))
                instantVel = instantVel.SetLength(minVelocity);
            else if (instantVel.LengthSquared() > (maxVelocity * maxVelocity))
                instantVel = instantVel.SetLength(maxVelocity);

            rigidbody.Velocity += instantVel * elapsedSeconds;
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("boomerang");
        }

        internal override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            RenderSprite(Position, sprite);
        }
    }
}
