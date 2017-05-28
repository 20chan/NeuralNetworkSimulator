using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeuralNetworkSimulator
{
    public class DrawLib
    {
        public static void DrawArrow(GraphicsDevice graphics, SpriteBatch sb, Vector2 v1, Vector2 v2, Color clr, float bold = 2f)
        {
            DrawLine(graphics, sb, v1, new Vector2((2 * v1.X + 8 * v2.X) / 10, (2 * v1.Y + 8 * v2.Y) / 10), clr, bold);
            DrawLine(graphics, sb, new Vector2((2 * v1.X + 8 * v2.X) / 10, (2 * v1.Y + 8 * v2.Y) / 10), v2, Color.DarkGreen, bold);
        }

        static Texture2D lineTexture;
        public static void DrawLine(GraphicsDevice graphics, SpriteBatch sb, Vector2 v1, Vector2 v2, Color clr, float bold = 1f)
        {
            if (lineTexture == null) lineTexture = new Texture2D(graphics, 1, (int)bold);
            Color[] pixels = new Color[(int)bold];
            for (int i = 0; i < bold; i++)
            {
                pixels[i] = Color.White;
            }
            lineTexture.SetData<Color>(pixels);

            float distance = Vector2.Distance(v1, v2);
            float angle = (float)Math.Atan2((double)(v2.Y - v1.Y), (double)(v2.X - v1.X));
            sb.Draw(lineTexture, v1, null, clr, angle, Vector2.Zero, new Vector2(distance, 1), SpriteEffects.None, 1.0f);
        }
    }
}