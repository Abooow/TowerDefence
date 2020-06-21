using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Screens;

namespace TowerDefence.Managers
{
    public static class ScreenManager
    {
        public static Screen CurrentScreen { get; private set; }

        private static Screen newScreen;
        private static bool switchScreen;

        public static void ChangeScreen(Screen newScreen)
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
