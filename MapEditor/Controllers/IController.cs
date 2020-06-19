﻿namespace MapEditor.Controllers
{
    public interface IController
    {
        bool Enabled { get; set; }
        void Update(float deltaTime);
    }
}
