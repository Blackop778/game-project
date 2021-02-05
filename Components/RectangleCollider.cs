using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Components
{
    public class RectangleCollider : Component
    {
        /// <summary>
        /// Top left corner of the rectangle
        /// </summary>
        public float X;
        /// <summary>
        /// Top left corner of the rectangle
        /// </summary>
        public float Y;
        private float _width;
        private float _halfwidth;
        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                _halfwidth = _width / 2;
            }
        }
        private float _height;
        private float _halfheight;
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                _halfheight = _height / 2;
            }
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        /// <summary>
        /// position is the top left corner of the rectangle
        /// </summary>
        public RectangleCollider(Actor attached, Vector2 center, float width, float height) : base(attached)
        {
            Width = width;
            Height = height;
            SetPositionFromCenterPoint(center);
        }

        internal override void Start()
        {
            base.Start();

            Game.Instance.AddCollider(this);
        }

        public void SetPositionFromCenterPoint(Vector2 center)
        {
            X = center.X - _halfwidth;
            Y = center.Y - _halfheight;
        }

        /// <summary>
        /// position is the top left corner of the rectangle
        /// </summary>
        public void UpdateDimensions(Vector2 center, Texture2D sprite)
        {
            Width = sprite.Width;
            Height = sprite.Height;
            SetPositionFromCenterPoint(center);
        }

        public bool CollidesWith(RectangleCollider other)
        {
            return CollisionHelper.Collides(this, other);
        }

        public void DoCollisionTriggers(RectangleCollider other)
        {
            Attached.OnTriggerEnter(this, other.Attached, other);
            if (other.Attached != null)
                other.Attached.OnTriggerEnter(other, Attached, this);
        }

        internal override void FixedUpdate()
        {
            base.FixedUpdate();

            SetPositionFromCenterPoint(Attached.Position);
        }
    }
}
