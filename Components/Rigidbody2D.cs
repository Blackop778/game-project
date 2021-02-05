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

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!float.IsInfinity(MaxAcceleration) && Acceleration.LengthSquared() > (MaxAcceleration * MaxAcceleration))
            {
                Acceleration = Acceleration.SetLength(MaxAcceleration);
            }
            Velocity += Acceleration * deltaTime;

            if (!float.IsInfinity(MaxVelocity) && Velocity.LengthSquared() > (MaxVelocity * MaxVelocity))
            {
                Velocity = Velocity.SetLength(MaxVelocity);
            }
            Attached.Position += Velocity * deltaTime;

            if (Drag != 0)
            {
                Velocity -= Drag * Velocity * deltaTime;
            }

        }
    }
}
