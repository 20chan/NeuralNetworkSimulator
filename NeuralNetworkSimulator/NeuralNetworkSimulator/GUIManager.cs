using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NeuralNetworkSimulator
{
    public class GUIManager
    {
        public List<GUI> Guis = new List<GUI>();

        public GUIManager()
        {

        }

        public void AddGui(GUI gui)
        {
            Guis.Add(gui);
        }

        public bool UpdateInput()
        {
            bool isUsedMouse = false;
            foreach (GUI gui in Guis)
            {
                if (gui.UpdateInput())
                    isUsedMouse = true;
            }
            return isUsedMouse;
        }

        public void Draw(GraphicsDevice gd, SpriteBatch sb)
        {
            foreach (GUI gui in Guis)
            {
                gui.Draw(gd, sb);
            }
        }
    }
}
