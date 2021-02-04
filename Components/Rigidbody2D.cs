using GameProject.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Components
{
    internal class Rigidbody2D : Component
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;
        public float MaxVelocity { get; set; } = float.PositiveInfinity;
        public float MaxAcceleration { get; set; } = float.PositiveInfinity;
        public float Drag { get; set; } = 0;

        public Rigidbody2D(Actor attached) : base(attached) { }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!float.IsInfinity(MaxAcceleration) && Acceleration.Length() > MaxAcceleration)
            {
                Acceleration = Vector2.Normalize(Acceleration);
                Acceleration *= MaxAcceleration;
            }
            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!float.IsInfinity(MaxVelocity) && Velocity.Length() > MaxVelocity)
            {
                Velocity = Vector2.Normalize(Velocity);
                Velocity *= MaxVelocity;
            }
            Attached.Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Drag != 0)
            {
                Vector2 dragVelocity = Velocity;
                if (Velocity.X != 0)
                {
                    dragVelocity.X += (float.IsNegative(Velocity.X) ? 1 : -1) * Drag * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Velocity.Y != 0)
                {
                    dragVelocity.Y += (float.IsNegative(Velocity.Y) ? 1 : -1) * Drag * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                Velocity = dragVelocity;
            }

        }
    }
}
