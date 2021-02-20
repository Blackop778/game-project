using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    class ExplosionEffect : Actor
    {
        private Texture2D spritesheet;
        private int animationFrame = 0;
        private float animationTime = 0f;

        private int spriteSize = 64;

        public ExplosionEffect(Vector2 position) : base(position) { }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            spritesheet = content.Load<Texture2D>("Explosion");
        }

        protected override void Update()
        {
            base.Update();

            animationTime += Time.DeltaTime;
            animationFrame = (int)(animationTime / .1f);
            if (animationFrame > 7)
                Destroy();
        }

        protected override void Draw()
        {
            base.Draw();

            RenderSpriteFromSheet(Position, spritesheet, spriteSize, spriteSize, 0, 0, animationFrame);
        }
    }
}
