using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NeuralNetworkSimulator
{
    public class GUI : GameObject
    {
        public GUI(Texture2D texture, Vector2 position, float scale = 1f) : base(texture, position, scale)
        {

        }

        public virtual void Clicked()
        {

        }

        public virtual bool UpdateInput() { return false; }
    }
}
