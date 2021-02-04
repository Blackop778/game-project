using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject
{
    static class InputManager
    {
        public static Vector2 Direction { get; private set; }
        public static bool Exit { get; private set; }

        private static KeyboardState currentKeyboardState;
        private static GamePadState currentGamePadState;

        public static void Update()
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(0);

            #region Direction
            Direction = currentGamePadState.ThumbSticks.Right;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-1, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(1, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -1);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, 1);
            }
            #endregion

            #region Exit
            Exit = currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape);
            #endregion
        }
    }
}
