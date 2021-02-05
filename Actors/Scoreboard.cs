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
        public int EnemiesKilled { get; set; } = 0;
        /// <summary>
        /// The most enemies killed in a single bannanarang
        /// </summary>
        public int EnemiesKilledStreak { get; set; } = 0;

        private SpriteFont bangers;

        public Scoreboard(Vector2 position) : base(position)
        {

        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            bangers = content.Load<SpriteFont>("bangers");
        }

        internal override void Draw()
        {
            base.Draw();

            RenderTextScreenspace(Position, $"Enemies killed: {EnemiesKilled}", bangers, Color.Gold);
            RenderTextScreenspace(Position + new Vector2(0, bangers.LineSpacing), $"Streak: {EnemiesKilledStreak}", bangers, Color.Gold);
        }
    }
}
