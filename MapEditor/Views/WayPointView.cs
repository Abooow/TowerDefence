using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapEditor.Controllers;
using MapEditor.Helpers;
using MapEditor.Managers;
using MapEditor.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Views
{
    public class WaypointView : IView
    {
        public bool Enabled { get; set; }

        private WaypointSelector pointSelector;
        private Texture2D selectedWaypointHightLight;
        private float waypointLayerDepth;

        public WaypointView(WaypointSelector pointSelector)
        {
            this.pointSelector = pointSelector;

            selectedWaypointHightLight = Circle.GetTexture(Game1.Graphics, (int)(WaypointManager.DefaultWaypointRadius * 2 * 1.3f));
            waypointLayerDepth = SortingOrder.GetLayerDepth(0, SortingLayer.WayPoints);

            Enabled = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < WaypointManager.WayPoints.Count; i++)
            {
                Waypoint point = WaypointManager.WayPoints[i];

                if (point == pointSelector.SelectedWaypoint)
                {
                    spriteBatch.Draw(
                        selectedWaypointHightLight,
                        point.Position - new Vector2(point.Radius * 1.3f),
                        null,
                        Color.DarkBlue,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        waypointLayerDepth - 0.001f);
                }

                spriteBatch.Draw(
                    point.Texture,
                    point.Position - new Vector2(point.Radius),
                    null,
                    Color.Red,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    waypointLayerDepth);

                spriteBatch.DrawString(
                    PositionInfoView.Font, 
                    i.ToString(), 
                    point.Position + new Vector2(0, -point.Radius * 1.5f), 
                    Color.White, 
                    0f, 
                    PositionInfoView.Font.MeasureString(i.ToString()) * new Vector2(0.5f, 1f),
                    1f, 
                    SpriteEffects.None,
                    waypointLayerDepth);
            }
        }
    }
}
