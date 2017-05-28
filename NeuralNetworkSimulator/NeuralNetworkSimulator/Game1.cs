using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NeuralNetworkSimulator
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static SpriteFont baseFont;
        public static GameManager GM = new GameManager();
        public static GUIManager GUIM = new GUIManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            GM.Circle = Content.Load<Texture2D>("Circle");
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0f);
            this.Window.AllowUserResizing = true;
            graphics.PreferMultiSampling = true;
            base.Initialize();
        }

        RoundButton playBtn;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            baseFont = Content.Load<SpriteFont>("SpriteFont1");

            playBtn = new RoundButton(Content.Load<Texture2D>("Play"), Content.Load<Texture2D>("Pause"), new Vector2(15, 15), 1f);
            GUIM.AddGui(playBtn);
        }
        
        protected override void UnloadContent()
        {
            
        }

        public static bool IsPause = false;

        bool isFirstSpace = true;
        bool isShowAllThresShold = false; bool wasPressingAlt = false;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && isFirstSpace)
            {
                ((RoundButton)GUIM.Guis[0]).ChangePauseMode(!IsPause);
                isFirstSpace = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.Space) && !isFirstSpace)
            {
                isFirstSpace = true;
            }
            if (!IsActive && !IsPause) playBtn.ChangePauseMode();

            if(Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && !wasPressingAlt)
            {
                wasPressingAlt = true;
                isShowAllThresShold = !isShowAllThresShold;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && wasPressingAlt)
                wasPressingAlt = false;

            bool isUsingMouse = GUIM.UpdateInput();
            GM.UpdateScroll(isShowAllThresShold);
            if (!IsPause)
            {
                GM.Update(isUsingMouse);

                if (gameTime.TotalGameTime.TotalMilliseconds % 10 == 0)
                {
                    GM.UpdateNeural();
                }
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            GM.Draw(this.GraphicsDevice, spriteBatch);
            GUIM.Draw(GraphicsDevice, spriteBatch);
            //spriteBatch.DrawString(baseFont, IsPause ? "Pause" : "Play", Vector2.Zero, Color.Black);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
