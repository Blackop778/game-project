using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Engine
{
    static class Utilities
    {
        public static bool IsOnScreen(Vector2 point)
        {
            return point.X >= 0 && point.X <= Game.Instance.DisplayWidth && point.Y >= 0 && point.Y <= Game.Instance.DisplayHeight;
        }
    }
}
