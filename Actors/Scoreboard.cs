using System;
using System.Collections.Generic;
using System.Text;
using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Actors
{
    class Scoreboard : Actor
    {
        public int Score { get; set; } = 0;
        /// <summary>
        /// The most enemies killed in a single bannanarang
        /// </summary>
        public int EnemiesKilledStreak { get; set; } = 0;

        private SpriteFont bangers;

        public Scoreboard(Vector2 position) : base(position)
        {

        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            bangers = content.Load<SpriteFont>("bangers");
        }

        protected override void Draw()
        {
            base.Draw();

            RenderTextScreenspace(Position, $"Score: {Score}", bangers, Color.Gold);
            RenderTextScreenspace(Position + new Vector2(0, bangers.LineSpacing), $"Highest killstreak: {EnemiesKilledStreak}", bangers, Color.Gold);
        }
    }
}
