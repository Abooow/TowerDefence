using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Controllers;
using TowerDefence.Moldels;
using TowerDefence.Views;

namespace TowerDefence.Managers
{
    public class ParticleManager : IController, IView
    {
        public bool Enabled { get; set; }
        public HashSet<int> ControllerGroupId { get;  }
        public HashSet<int> ViewGroupId { get;  }

        private List<Particle> particles;

        public ParticleManager()
        {
            particles = new List<Particle>();

            ControllerGroupId = new HashSet<int>();
            ViewGroupId = new HashSet<int>();
            Enabled = true;
        }

        public void AddParticle(Particle particle)
        {
            if (Enabled && !particles.Contains(particle)) particles.Add(particle);
        }

        public void Update(float deltaTime)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                if (particles[i].Update(deltaTime)) particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
} 
