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
    public class RoundButton : Button
    {
        public bool isPause = true;
        public float Radius { get; set; }
        Texture2D PlayTexture;
        Texture2D PauseTexture;
        public RoundButton(Texture2D playTexture, Texture2D pauseText, Vector2 position, float scale = 1f) : base(pauseText, position, scale)
        {
            this.Radius = playTexture.Width * scale / 2;

            this.PlayTexture = playTexture;
            this.PauseTexture = pauseText;
        }

        public override bool IsClicking()
        {
            return Vector2.Distance(this.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)) <= Radius;
        }

        public override void Clicked()
        {
            ChangePauseMode();
            base.Clicked();
        }

        public void ChangePauseMode()
        {
            isPause = !isPause;
            Game1.IsPause = isPause;
            this.Texture = isPause ? PauseTexture : PlayTexture;
        }

        public void ChangePauseMode(bool ispause)
        {
            isPause = ispause;
            Game1.IsPause = ispause;
            this.Texture = isPause ? PauseTexture : PlayTexture;
        }

        public override void MouseDown()
        {
            base.MouseDown();
        }

        public override void MouseUp()
        {
            base.MouseUp();
        }
    }
}
