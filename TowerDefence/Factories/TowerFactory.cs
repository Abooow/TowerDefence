using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Towers;

namespace TowerDefence.Factories
{
    public static class TowerFactory
    {
        public static Dictionary<string, Tower> Towers { get; private set; } = new Dictionary<string, Tower>();
        public static Tower DefaultTower { get; set; }

        public static void Add(string name, Tower tower)
        {
            if (!Towers.ContainsKey(name))
            {
                Towers.Add(name, tower);
                if (DefaultTower == null)
                    DefaultTower = tower;
            }
        }

        public static Tower GetTower(string name)
        {
            Tower towerToCopy = DefaultTower;
            if (Towers.ContainsKey(name))
                towerToCopy = Towers[name];

            return towerToCopy.Duplicate();
        }
    }
}
