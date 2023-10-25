using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Animation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _greyTribble, _orangeTribble, _brownTribble, _creamTribble, _tribbleIntro;
        SpriteFont _introText;
        private bool gray = false, orange = false, brown = false, cream = false;
        private int bounces = 0;
        Color backColor = Color.White;
        Random generator = new Random();
        SoundEffect _tribbleCoo;
        MouseState mouseState;
        Tribble tribbleGrey, tribbleOrange, tribbleBrown, tribbleCream;
        Rectangle introRect;

        enum Screen
        {
            Intro,
            TribbleYard
        }
        Screen screen = Screen.Intro;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
            introRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            this.Window.Title = "Animation";
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _greyTribble = Content.Load<Texture2D>("tribbleGrey");
            _orangeTribble = Content.Load<Texture2D>("tribbleOrange");
            _brownTribble = Content.Load<Texture2D>("tribbleBrown");
            _creamTribble = Content.Load<Texture2D>("tribbleCream");
            _tribbleCoo = Content.Load<SoundEffect>("tribble_coo");
            _tribbleIntro = Content.Load<Texture2D>("tribble_intro");
            _introText = Content.Load<SpriteFont>("Intro");
            tribbleGrey = new Tribble(_greyTribble, new Rectangle(generator.Next(1, 690), generator.Next(1, 390), generator.Next(50, 151), generator.Next(50, 151)), new Vector2(3, 3));
            tribbleOrange = new Tribble(_orangeTribble, new Rectangle(generator.Next(1, 690), generator.Next(1, 390), generator.Next(50, 151), generator.Next(50, 151)), new Vector2(2, 4));
            tribbleBrown = new Tribble(_brownTribble, new Rectangle(generator.Next(1, 690), generator.Next(1, 390), generator.Next(50, 151), generator.Next(50, 151)), new Vector2(6, 0));
            tribbleCream = new Tribble(_creamTribble, new Rectangle(generator.Next(1, 690), generator.Next(1, 390), generator.Next(50, 151), generator.Next(50, 151)), new Vector2(0, 6));
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;
            }
            else if (screen == Screen.TribbleYard)
            {
                if (tribbleGrey.Move(_graphics))
                {
                    backColor = Color.Gray;
                    _tribbleCoo.Play();
                    bounces++;
                }
                if (tribbleOrange.Move(_graphics))
                {
                    backColor = Color.Orange;
                    _tribbleCoo.Play();
                    bounces++;
                }
                if (tribbleBrown.Move(_graphics))
                {
                    backColor = Color.SaddleBrown;
                    _tribbleCoo.Play();
                    bounces++;
                }
                if (tribbleCream.Move(_graphics))
                {
                    backColor = Color.AntiqueWhite;
                    _tribbleCoo.Play();
                    bounces++;
                }
                if (bounces >= 50)
                {
                    screen = Screen.Intro;
                    bounces = 0;
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backColor);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(_tribbleIntro, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(_introText, "Click to Tribble it up", new Vector2(0, 250), Color.Black);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(tribbleGrey.Texture, tribbleGrey.Bounds, Color.White);
                _spriteBatch.Draw(tribbleOrange.Texture, tribbleOrange.Bounds, Color.White);
                _spriteBatch.Draw(tribbleBrown.Texture, tribbleBrown.Bounds, Color.White);
                _spriteBatch.Draw(tribbleCream.Texture, tribbleCream.Bounds, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}