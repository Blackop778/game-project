using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Engine
{
    static class Debug
    {
        public static readonly bool DISPLAY_COLLIDERS = false; 
        public static void Log(string output)
        {
            System.Diagnostics.Debug.WriteLine(output);
        }
    }
}
