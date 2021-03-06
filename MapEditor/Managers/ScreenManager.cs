﻿using Microsoft.Xna.Framework.Graphics;
using MapEditor.Screens;

namespace MapEditor.Managers
{
    public static class ScreenManager
    {
        public static BaseScreen CurrentScreen { get; private set; }

        private static BaseScreen newScreen;
        private static bool switchScreen;

        public static void ChangeScreen(BaseScreen newScreen)
        {
            if (CurrentScreen == null)
            {
                CurrentScreen = newScreen;
            }
            else
            {
                ScreenManager.newScreen = newScreen;
                switchScreen = true;
            }
        }

        public static void Update(float deltaTime)
        {
            if (switchScreen)
            {
                CurrentScreen = newScreen;
                switchScreen = false;
            }

            CurrentScreen?.Update(deltaTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen?.Draw(spriteBatch);
        }
    }
}
