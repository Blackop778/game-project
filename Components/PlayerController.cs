using GameProject.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Components
{
    class PlayerController : Component
    {
        public float Acceleration { get; set; } = 300;

        private Rigidbody2D rigidbody;

        public PlayerController(Actor attached) : base(attached)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 instantVel = InputManager.Direction * Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Make stopping faster
            if (Vector2.Dot(Vector2.Normalize(instantVel), Vector2.Normalize(rigidbody.Velocity)) < 0.25f)
            {
                instantVel *= 1.5f;
            }

            rigidbody.Velocity += instantVel;
        }
    }
}
