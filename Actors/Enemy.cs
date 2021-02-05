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
    class Enemy : Actor
    {
        private Texture2D sprite;
        private RectangleCollider collider;

        public Enemy(Vector2 position) : base(position)
        {

        }

        internal override void Start()
        {
            base.Start();

            collider = AddComponent(new RectangleCollider(this, Position, 64, 64));
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("enemy");
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

            if (other is Boomerang)
            {
                Destroy();
                Game.Instance.SpawnEnemy();
            }
        }

        internal override void Update()
        {

        }
    }
}
