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
        private RectangleCollider collider;

        private readonly float maxVelocity = 600;
        private readonly float minVelocity = 400;
        private readonly float maximumInteractRange = 50;

        public Boomerang(Vector2 position, Vector2 destination, Actor user) : base(position) {
            this.destination = destination;
            this.user = user;
        }

        internal override void Start()
        {
            base.Start();

            rigidbody = AddComponent(new Rigidbody2D(this));
            rigidbody.MaxVelocity = maxVelocity;
            rigidbody.Drag = 77f;

            collider = AddComponent(new RectangleCollider(this, Position, 16, 16));

            returning = false;
        }

        internal override void Update()
        {
            
        }

        internal override void FixedUpdate()
        {
            if (returning)
                destination = user.Position;

            if (Vector2.Distance(Position, destination) < maximumInteractRange)
            {
                if (!returning)
                    returning = true;
            }

            Vector2 instantVel = destination - Position;

            if (instantVel.LengthSquared() < (minVelocity * minVelocity))
                instantVel = instantVel.SetLength(minVelocity);
            else if (instantVel.LengthSquared() > (maxVelocity * maxVelocity))
                instantVel = instantVel.SetLength(maxVelocity);

            rigidbody.Velocity += instantVel * Time.fixedDeltaTime;
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("boomerang");
            collider.UpdateDimensions(Position, sprite);
        }

        internal override void Draw()
        {
            base.Draw();

            RenderSprite(Position, sprite);
        }

        internal override void OnTriggerEnter(RectangleCollider collider, Actor other, RectangleCollider otherCollider)
        {
            base.OnTriggerEnter(collider, other, otherCollider);

            if (returning && other == user)
                Destroy();
        }
    }
}
