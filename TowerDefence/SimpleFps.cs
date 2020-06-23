// This class was copied from https://community.monogame.net/t/a-simple-monogame-fps-display-class/10545

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    public class SimpleFps
    {
        private double frames = 0;
        private double elapsed = 0;
        public double msgFrequency = 1f;
        public string msg = " ";

        /// <summary>
        /// The msgFrequency here is the reporting time to update the message.
        /// </summary>
        public void Update(float deltaTime)
        {
            elapsed += deltaTime;
            if (elapsed > msgFrequency)
            {
                msg = $"Fps: {(frames / elapsed):.0} Elapsed time: {elapsed:0.0}";
                elapsed = 0;
                frames = 0;
            }
            frames++;
        }

        public void DrawFps(SpriteBatch spriteBatch, SpriteFont font, Vector2 fpsDisplayPosition, Color fpsTextColor)
        {
            spriteBatch.DrawString(font, msg, fpsDisplayPosition, fpsTextColor);
            frames++;
        }
    }
}