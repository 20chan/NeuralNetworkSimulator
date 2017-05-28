using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NeuralNetworkSimulator
{
    public class Neural : GameObject
    {
        public List<Arrow> LinkedObjects = new List<Arrow>();

        public bool isActivate = false; //NEURAL NETWORK - ELECTRIC
        public bool isReadyActivate = false;
        public int neuralCode = 0; bool isClicking = false;

        public float Threshold { get; set; } = 1f;
        private float currentInput = 0f;

        public Neural(Texture2D texture, Vector2 position,float threshold = 1f) : base(texture, position, 1f)
        {
        }

        bool isLinking = false; public bool isLinkFinishedJustNow = false;
        bool isMouseUpAfterLinkClicked = false;
        bool wasPressingControl = false;
        public void UpdatePhysics(bool wasUsingLMouse, ref bool isUsingLMouse, bool wasUsingRMouse, ref bool isUsingRMouse)
        {
            
            position += velocity;
            velocity -= velocity * friction;

            if (!isLinking && !wasUsingLMouse && !wasUsingRMouse && IsClicking() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isClicking = true;
            }
            if (isClicking)
            {
                isUsingLMouse = true;
                AddForceTo(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            }
            if (isClicking && Mouse.GetState().LeftButton == ButtonState.Released)
                isClicking = false;

            if (!isLinking && !wasUsingLMouse && !wasUsingRMouse && IsClicking() && Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                isLinking = true;
                isMouseUpAfterLinkClicked = false;
            }
            if (isLinking && !isMouseUpAfterLinkClicked && Mouse.GetState().RightButton == ButtonState.Released)
            {
                isMouseUpAfterLinkClicked = true;
            }
            if (isLinkFinishedJustNow) Game1.GM.dontMakeNewNeural = true;
            if (isLinking && !wasUsingLMouse && isMouseUpAfterLinkClicked && !wasUsingRMouse && IsClicking() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isLinking = false;
                isMouseUpAfterLinkClicked = false;
                Game1.GM.dontMakeNewNeural = true;
            }
            if (isLinkFinishedJustNow) isLinkFinishedJustNow = false;
            if (isLinking && !wasUsingLMouse && isMouseUpAfterLinkClicked && !wasUsingRMouse && !IsClicking() && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isLinking = false;
                isMouseUpAfterLinkClicked = false;
                isLinkFinishedJustNow = true;

                if (Game1.GM.ClickingNeuralCode() == -1) return;
                LinkedObjects.Add(new Arrow(Game1.GM.ClickingNeuralCode(), 1f));
            }
            if (!isLinking && !isClicking && Keyboard.GetState().IsKeyDown(Keys.LeftControl) && IsClicking() && !wasPressingControl)
            {
                this.GiveSignal(1f);
                wasPressingControl = true;
            }
            if(!isLinking && !isClicking && Keyboard.GetState().IsKeyUp(Keys.LeftControl) && IsClicking() && wasPressingControl)
            {
                wasPressingControl = false;
            }
            
        }
        
        public void UpdateNeural()
        {
            if (currentInput + 0.009f >= Threshold) Activate();

            if (isActivate)
            {
                foreach (Arrow code in LinkedObjects)
                {
                    Game1.GM.Neurals[code.ToNeuralCode].GiveSignal(code.Weight * currentInput);
                    //Game1.GM.Neurals[code.ToNeuralCode].Activate();
                }
                currentInput = 0f;
                isActivate = false;
            }
            
            if (isReadyActivate)
            {
                isReadyActivate = false;
                isActivate = true;
            }
            neuralCode++;
            if (LinkedObjects.Count == 0) currentInput = 0f;
        }

        int scroll = 0;
        static bool showAllThreshold = false;
        public void UpdateThreshold(bool isShowAllThreShold)
        {
            if (IsClicking())
                if (scroll != Mouse.GetState().ScrollWheelValue)
                {
                    if (scroll - Mouse.GetState().ScrollWheelValue < 0)
                    {
                        Threshold += Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 0.1f : 0.01f;
                    }
                    else
                    {
                        Threshold -= Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 0.1f : 0.01f;
                    }
                }

            scroll = Mouse.GetState().ScrollWheelValue;
            showAllThreshold = isShowAllThreShold;
        }

        public void GiveSignal(float sig) //아 왜자꾸 sig 아 0이 되지 ;; 분명 currentInput 이 0이 되는 버그때문일텐데;;
        {
            currentInput += sig;
            //if (currentInput >= Threshold) Activate();
        }

        private void Activate()
        {
            if (Game1.GM.neuralCode == neuralCode)
                isActivate = true;
            else
                isReadyActivate = true;
        }

        public override void Draw(GraphicsDevice gd, SpriteBatch sb)
        {
            if(showAllThreshold)
                sb.DrawString(Game1.baseFont, Threshold.ToString("0.00"), new Vector2(Position.X - 20, Position.Y - 30), Color.Black, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            
            else if (IsClicking())
            {
                sb.DrawString(Game1.baseFont, Threshold.ToString("0.00"), new Vector2(Position.X - 20, Position.Y - 30), Color.Black, 0f, new Vector2(), 1f, SpriteEffects.None, 1f);
            }
            if (isActivate)
                sb.Draw(Texture, Position - GetCenter(), null, Color.Yellow, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
            else if (isReadyActivate)
                sb.Draw(Texture, Position - GetCenter(), null, Color.Red, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
            else if (isClicking)
                sb.Draw(Texture, Position - GetCenter(), null, Color.Blue, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
            else
                base.Draw(gd, sb);
            if (isLinking)
                DrawLib.DrawArrow(gd, sb, Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.DarkBlue);
            foreach (Arrow code in LinkedObjects)
            {
                if (isActivate)
                    DrawLib.DrawArrow(gd, sb, Position, Game1.GM.Neurals[code.ToNeuralCode].Position, Color.Yellow);
                else
                    DrawLib.DrawArrow(gd, sb, Position, Game1.GM.Neurals[code.ToNeuralCode].Position, Color.DarkBlue);
            }
        }
    }

    public struct Arrow
    {
        public Arrow(int to, float weight)
        {
            ToNeuralCode = to;
            Weight = weight;
        }
        public int ToNeuralCode;
        public float Weight;
    }
}
