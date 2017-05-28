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
    public class Button : GUI
    {
        public Button(Texture2D texture, Vector2 position, float scale = 1f) : base(texture, position, scale)
        {
            originalScale = Scale;
        }

        readonly float originalScale;
        public virtual void MouseDown()
        {
            this.Scale = originalScale * 0.9f;
        }

        public virtual void MouseUp()
        {
            this.Scale = originalScale;
        }

        bool wasMouseDown = false;

        public override bool UpdateInput()
        {
            if(IsClicking() && Mouse.GetState().LeftButton == ButtonState.Pressed && !wasMouseDown)
            {
                Clicked();
                wasMouseDown = true;
                MouseDown();
                return true;
            }
            if(wasMouseDown && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                MouseUp();
                wasMouseDown = false;
            }
            return false;
        }
    }
}
