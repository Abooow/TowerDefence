using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence.Helpers
{
    public static class CollectionExtensions
    {
        public static bool ContainsAny<T>(this ICollection<T> collection, ICollection<T> objects)
        {
            if (objects == null) return false;

            foreach (T item in collection)
            {
                if (objects.Contains(item)) return true;
            }
            return false;
        }
    }
}
