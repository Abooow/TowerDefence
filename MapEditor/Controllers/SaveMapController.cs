using MapEditor.Managers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using MapEditor.Helpers;

namespace MapEditor.Controllers
{
    public class SaveMapController : IController
    {
        public bool Enabled { get; set; }

        private KeyboardState lastKeyboard;

        public SaveMapController()
        {
            lastKeyboard = Keyboard.GetState();

            Enabled = true;
        }

        public void Update(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1) && lastKeyboard.IsKeyUp(Keys.F1))
            {
                SaveFileDialog saveFile = new SaveFileDialog()
                {
                    Filter = "Xml files|*.xml|All Files|*.*"
                };
                if (saveFile.ShowDialog() == true)
                {
                    SaveMap.Save(saveFile.FileName);
                }
            }

            lastKeyboard = Keyboard.GetState();
        }
    }
}
