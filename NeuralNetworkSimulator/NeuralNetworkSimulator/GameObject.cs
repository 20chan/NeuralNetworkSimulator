using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NeuralNetworkSimulator
{
    public class GameObject
    {
        public int Code = 0;

        public Texture2D Texture { get; set; }
        protected Vector2 position = new Vector2();
        protected Vector2 velocity = new Vector2();
        protected float friction = 0.3f;
        public float Scale { get; set; } = 1f;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 GetCenter() { return new Vector2(Texture.Width * Scale / 2, Texture.Height * Scale / 2); }
        public Rectangle GetBounds() { return new Rectangle((int)(Position.X - GetCenter().X), (int)(Position.Y - GetCenter().Y), (int)(GetCenter().X * 2), (int)(GetCenter().Y * 2)); }
        
        public GameObject(Texture2D texture, Vector2 position, float scale = 1f)
        {
            this.Texture = texture;
            Position = position;
            this.Scale = scale;
        }
        
        public virtual void Draw(GraphicsDevice gd, SpriteBatch sb)
        {
            sb.Draw(Texture, Position - GetCenter(), null, Color.White, 0f, new Vector2(), Scale, SpriteEffects.None, 0f);
           
        }

        public virtual bool IsClicking()
        {
            if (GetBounds().Contains(Mouse.GetState().X, Mouse.GetState().Y))
                return true;
            else
                return false;
        }

        public void AddForce(Vector2 vec)
        {
            velocity += vec;
        }

        public void AddForceTo(Vector2 vec)
        {
            AddForce(vec - Position);
        }
    }
}