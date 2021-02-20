using GameProject.Engine;
using GameProject.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    class Bomb : Actor
    {
        private RectangleCollider collider;
        private Texture2D sprite;
        private SoundEffect explosion;

        public Bomb(Vector2 position) : base(position) { }

        internal override void Start()
        {
            base.Start();

            collider = AddComponent(new RectangleCollider(this, Position, 64, 64));
        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("Bomb");
            collider.UpdateDimensions(Position, sprite);

            explosion = content.Load<SoundEffect>("bomb_explosion");
        }

        protected override void Draw()
        {
            base.Draw();

            RenderSprite(Position, sprite);
        }

        internal override void OnTriggerEnter(RectangleCollider collider, Actor other, RectangleCollider otherCollider)
        {
            base.OnTriggerEnter(collider, other, otherCollider);

            if (other is Boomerang)
            {
                other.Destroy();
                Destroy();
                explosion.Play();
                Instantiate(new ExplosionEffect(Position));
            }
        }

        public override void FinalDestroy()
        {
            base.FinalDestroy();

            collider = null;
        }
    }
}
