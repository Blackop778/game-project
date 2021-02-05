using GameProject.Engine;
using GameProject.Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    class PlayerBlocker : Actor
    {
        private readonly Direction direction;

        /// <summary>
        /// Janky hack for keeping the player from moving into an area
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction">Where this blocker is relative to the screen</param>
        public PlayerBlocker(Vector2 position, Direction direction) : base(position)
        {
            this.direction = direction;
        }

        internal override void Start()
        {
            base.Start();

            float width = 1000;
            float height = 1000;

            if (direction == Direction.Top || direction == Direction.Bottom)
                width = Game.Instance.DisplayWidth;
            else
                height = Game.Instance.DisplayHeight;

            AddComponent(new RectangleCollider(this, Position, width, height));
        }

        internal override void OnTriggerEnter(RectangleCollider collider, Actor other, RectangleCollider otherCollider)
        {
            base.OnTriggerEnter(collider, other, otherCollider);

            if (other is Player p)
            {
                if (direction == Direction.Top || direction == Direction.Bottom)
                {
                    other.Position = new Vector2(other.Position.X,
                        direction == Direction.Top ? collider.Bottom - (otherCollider.HalfHeight + 2) : collider.Top + (otherCollider.HalfHeight + 2));
                    p.Rigidbody.Velocity = new Vector2(p.Rigidbody.Velocity.X, 0);
                }
                else
                {
                    other.Position = new Vector2(
                        direction == Direction.Right ? collider.Left - (otherCollider.HalfWidth + 2) : collider.Right + (otherCollider.HalfWidth + 2),
                        other.Position.Y);
                    p.Rigidbody.Velocity = new Vector2(0, p.Rigidbody.Velocity.Y);
                }
            }
        }
    }
}
