using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Managers;
using TowerDefence.Views;

namespace TowerDefence.Controllers
{
    public class AvailableMapsScrollController : IController
    {
        public bool Enabled { get; set; }

        private AvailableMapsView mapsView;

        private MouseState lastMouse;

        public AvailableMapsScrollController(AvailableMapsView mapsView)
        {
            this.mapsView = mapsView;

            lastMouse = Mouse.GetState();

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            int delta = Mouse.GetState().ScrollWheelValue - lastMouse.ScrollWheelValue;
            if (delta != 0)
                mapsView.XOffset = MathHelper.Clamp(mapsView.XOffset + delta * 0.5f, mapsView.MaxScrollValue, mapsView.MinScrollValue);

            lastMouse = Mouse.GetState();
        }
    }
}
