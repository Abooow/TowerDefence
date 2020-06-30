using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.Controllers;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Towers;

namespace TowerDefence.Views.UI
{
    public class AvailableTowersUiView : IUiView
    {
        public HashSet<int> ViewGroupId { get; }
        public bool Enabled { get; set; }
        public Rectangle BackgroundRect { get; set; }
        public Vector2 ButtonsOffset { get; set; }
        public List<Button> Buttons { get; private set; }

        private ButtonManager buttonManager;

        public AvailableTowersUiView(ButtonManager buttonManager, TowerSelectorController towerSelector, Rectangle backgroundRect)
        {
            this.buttonManager = buttonManager;
            BackgroundRect = backgroundRect;
            ButtonsOffset = backgroundRect.Location.ToVector2();

            Buttons = new List<Button>();
            Buttons.Add(new TowerButton(
                ButtonsOffset + new Vector2(10f, 30f),
                new Vector2(100f, 80f),
                new TestTower(null, null),
                new Rectangle(0, 0, 100, 60),
                200)
            { 
                TextPosition = new Vector2(50f, 70f),
                AttachedObject = "Tower1"
            });
            Buttons[0].OnClicked += towerSelector.OnTowerButtonClicked;

            buttonManager.AddButtons(Buttons);

            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            buttonManager.Update(0f);

            spriteBatch.Draw(
                AssetManager.GetTexture("Pixel"),
                BackgroundRect,
                null,
                Color.BurlyWood,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            buttonManager.Draw(spriteBatch);
        }

        private void OnButtonClicked(object obj, EventArgs args)
        {
            TowerButton button = obj as TowerButton;
            Console.WriteLine("Clicked on:" + button.AttachedObject);
        }

        class TowerButton : Button
        {
            public int Cost { get; set; }
            public Rectangle ImageRect { get; set; }
            public Vector2 TextPosition { get; set; }

            private Tower tower;
            private float layerDepth;

            public TowerButton(Vector2 position, Vector2 size, Tower tower, Rectangle imageRect, int cost)
                : base(position, size)
            {
                this.tower = tower;
                ImageRect = imageRect;
                Cost = cost;

                layerDepth = LayerDepth + 0.001f;
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);

                Vector2 towerPos = Position + ImageRect.Location.ToVector2() + ImageRect.Size.ToVector2() * 0.5f;
                tower.Position = towerPos;
                tower.Draw(spriteBatch, Color.White);

                string costText = Cost.ToString();
                spriteBatch.DrawString(
                    AssetManager.GetFont("BaseFont"),
                    costText,
                    Position + TextPosition,
                    Color.White,
                    0f,
                    AssetManager.GetFont("BaseFont").MeasureString(costText) * new Vector2(0.5f),
                    1f,
                    SpriteEffects.None,
                    layerDepth);
            }
        }
    }
}
