using GameProject.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Engine.Components
{
    internal class Rigidbody2D : Component
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;
        public float MaxVelocity { get; set; } = float.PositiveInfinity;
        public float MaxAcceleration { get; set; } = float.PositiveInfinity;
        public float Drag { get; set; } = 0;

        public Rigidbody2D(Actor attached) : base(attached) { }

        internal override void FixedUpdate()
        {
            base.Update();

            float deltaTime = Time.fixedDeltaTime;

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

            float velocityMag;
            if (Drag != 0 && (velocityMag = Velocity.LengthSquared()) != 0)
            {
                Vector2 newVelocity = Velocity;

                newVelocity.X += (newVelocity.X * newVelocity.X) / velocityMag * Drag * deltaTime * (float.IsNegative(Velocity.X) ? 1 : -1);
                newVelocity.Y += (newVelocity.Y * newVelocity.Y) / velocityMag * Drag * deltaTime * (float.IsNegative(Velocity.Y) ? 1 : -1);

                Velocity = newVelocity;
            }

        }
    }
}
