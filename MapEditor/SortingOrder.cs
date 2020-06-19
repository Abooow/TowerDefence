using System;

namespace MapEditor
{
    public static class SortingOrder
    {
        public static float GetLayerDepth(int sortingOrder, SortingLayer layer)
        {
            return ((float)layer * 1000f + sortingOrder + 1000f) / (Enum.GetValues(typeof(SortingLayer)).Length * 1000f + 1000f);
        }
    }

    public enum SortingLayer
    {
        Map,
        WayPoints
    }
}
