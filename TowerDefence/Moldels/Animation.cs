using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TowerDefence.Moldels
{
    public class Animation<T>
    {
        public T Value { get; private set; }

        public bool Running { get; private set; }
        public bool Loop { get; set; }

        public bool Automatic { get; set; }
        public bool HaveLoopedOnce { get; private set; }

        private T[] values;
        private int index;

        private int interval;
        private double timer;

        public Animation(T[] values)
        {
            this.values = values;

            Value = values[0];
            Running = false;
            Loop = true;
            Automatic = false;
            HaveLoopedOnce = false;
            index = 0;
            interval = 100;
        }
        public Animation(T[] values, float duration) : this(values)
        {
            interval = (int)(duration * 1000 / values.Length);
            Automatic = true;
        }
        public Animation(T[] values, int fps) : this(values)
        {
            interval = 1000 / fps;
            Automatic = true;
        }

        public bool IsAtEnd() => index == values.Length - 1;

        public void Reset()
        {
            timer = 0;
            index = 0;
            Value = values[0];
            HaveLoopedOnce = false;
        }

        public void Start()
        {
            if (!Running)
            {
                Reset();

                if (Automatic) Task.Run(Animate);
            }

            Running = true;
        }

        public void Update()
        {
            if (!Running) return;

            index++;
            if (index == values.Length)
            {
                HaveLoopedOnce = true;

                if (Loop)
                    index = 0;
                else
                    Running = false;
            }
            Value = values[index];
        }

        public void Update(double elapsed)
        {
            if (!Running) return;

            Automatic = false;
            timer += elapsed;
            if (timer >= interval)
            {
                timer = 0;
                Update();
            }
        }

        public void Stop() => Running = false;

        private void Animate()
        {
            while (true)
            {
                if (!Running) break;

                Thread.Sleep(interval);

                index++;
                if (index == values.Length)
                {
                    HaveLoopedOnce = true;

                    if (Loop)
                        index = 0;
                    else
                        break;
                }

                Value = values[index];
            }

            Running = false;
        }
    }
}
