﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.Views
{
    public interface IView
    {
        HashSet<int> ViewGroupId { get; }
        bool Enabled { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
