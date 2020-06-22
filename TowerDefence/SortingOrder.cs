using System;

namespace TowerDefence
{
    public static class SortingOrder
    {
        public static float GetLayerDepth(int sortingOrder, SortingLayer layer)
        {
            return ((float)layer * 500f + sortingOrder + 500f) / (Enum.GetValues(typeof(SortingLayer)).Length * 500f + 500f);
        }
    }

    public enum SortingLayer
    {
        Background,
        Ground,
        TowerBase,
        Enemies,
        Bullets,
        TowerTop,
        Flying,
        Ui,
        UiText,
        Popups,
        PopupsText
    }
}
