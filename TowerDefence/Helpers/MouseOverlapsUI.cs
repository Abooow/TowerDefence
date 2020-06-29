using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using TowerDefence.Views.UI;

namespace TowerDefence.Helpers
{
    public static class MouseOverlapsUI
    {
        public static AvailableTowersUiView AvailableTowersUi;

        public static bool IsMouseOverUI()
        {
            return AvailableTowersUi?.BackgroundRect.Contains(Mouse.GetState().Position) ?? false;
        }
    }
}
