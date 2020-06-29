using System.Collections.Generic;

namespace TowerDefence.Controllers
{
    public interface IController
    {
        HashSet<int> ControllerGroupId { get; }
        bool Enabled { get; set; }
        void Update(float deltaTime);
    }
}
