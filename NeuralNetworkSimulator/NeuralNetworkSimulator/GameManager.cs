using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NeuralNetworkSimulator
{
    public enum GameMode
    {
        NEURALEDIT
    }
    public enum NeuralMode
    {
        NEURAL
    }
    public class GameManager
    {
        public Texture2D Circle;
        bool isUsingLMouse = false, isUsingRMouse = false;
        bool wasUsingLMouse = false, wasUsingRMouse = false;
        public List<Neural> Neurals = new List<Neural>();
        public int neuralCode = 0;

        public bool dontMakeNewNeural = false;

        public void Update(bool isUsingMouse)
        {
            foreach(Neural g in Neurals)
            {
                g.UpdatePhysics(wasUsingLMouse, ref isUsingLMouse, wasUsingRMouse, ref isUsingRMouse);
            }
            foreach (Neural g in Neurals)
                if (g.isLinkFinishedJustNow)
                    dontMakeNewNeural = true;
            if (!dontMakeNewNeural && !isUsingMouse && !isUsingLMouse && Mouse.GetState().LeftButton == ButtonState.Pressed) //FUCK THIS SHIT DONTMAKENEWNEURLA DOESN'T WORK
            {
                AddNeural(new Neural(Circle, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 100));
            }
            wasUsingLMouse = isUsingLMouse;
            isUsingLMouse = false;
            dontMakeNewNeural = false;
        }

        public void UpdateNeural()
        {
            neuralCode++;
            foreach(Neural g in Neurals)
            {
                g.UpdateNeural();
            }
        }

        public void UpdateScroll(bool isShowAllThreshold)
        {
            foreach(Neural g in Neurals)
            {
                g.UpdateThreshold(isShowAllThreshold);
            }
        }

        public void Draw(GraphicsDevice gd, SpriteBatch sb)
        {
            foreach(Neural g in Neurals)
            {
                g.Draw(gd, sb);
            }
        }

        public void AddNeural(Neural Neural)
        {
            Neural.Code = Neurals.Count;
            this.Neurals.Add(Neural);
        }

        public void DeleteNeural(int Code)
        {
            Neurals.RemoveAt(Code);
        }

        public int ClickingNeuralCode()
        {
            foreach (Neural g in Neurals)
            {
                if (g.IsClicking()) return g.Code;
            }
            return -1;
        }
    }
}
