using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;
using TowerDefence.Moldels;
using TowerDefence.Screens;
using TowerDefence.Views;

namespace TowerDefence.Controllers
{
    public class SelectMapController : IController
    {
        public HashSet<int> ControllerGroupId { get; }
        public bool Enabled { get; set; }

        private AvailableMapsView mapsView;
        private ButtonManager buttonManager;

        private List<Button> buttons;

        public SelectMapController(AvailableMapsView mapsView, ButtonManager buttonManager)
        {
            this.mapsView = mapsView;
            this.buttonManager = buttonManager;

            buttons = new List<Button>();
            for (int i = 0; i < MapManager.TotalLoadedMaps; i++)
            {
                buttons.Add(new Button(Vector2.Zero, mapsView.ThumbnailSize.ToVector2())
                {
                    AttachedObject = i,
                    Visible = false
                });
                buttons[i].OnClicked += OnButtonClicked;
            }
            buttonManager.AddButtons(buttons);

            ControllerGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            for (int i = 0; i < MapManager.TotalLoadedMaps; i++)
            {
                Point position = new Point(
                    (int)mapsView.XOffset + (i / 2) * (mapsView.ThumbnailSize.X + mapsView.Margin.X),
                    (int)mapsView.YOffset + (i % 2) * (mapsView.ThumbnailSize.Y + mapsView.Margin.Y));

                buttons[i].Position = position.ToVector2();
            }
        }

        private void OnButtonClicked(object obj, EventArgs eventArgs)
        {
            Console.WriteLine($"Loading map: {((Button)obj).AttachedObject}");
            MapManager.LoadMap(MapManager.CreateMap(MapManager.Maps[(int)((Button)obj).AttachedObject], Game1.Graphics));
            ScreenManager.ChangeScreen(new TestScreen());
        }
    }
}
