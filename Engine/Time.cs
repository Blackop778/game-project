using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Engine
{
    public static class Time
    {
        public static float deltaTime;
        public static float fixedDeltaTime = 0.01f;

        public static void UpdateTime(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
