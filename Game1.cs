using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MathematicalPendulum
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D circleTexture;
        private Texture2D rectangle;
        private SpriteFont sFont;

        internal Vector2 origin;
        internal Vector2 endpoint;
        // 100 pixels = 1m

        internal double theta = Math.PI + 0.001;
        internal double omega = 0;
        internal double g = 9.81;
        internal double length = 200;
        internal double mu = 0.2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            circleTexture = Content.Load<Texture2D>("circle");
            sFont = Content.Load<SpriteFont>("Font");

            origin = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            rectangle = new Texture2D(GraphicsDevice, (int)length, 1);
            var data = new Color[(int)length * 1];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }
            rectangle.SetData(data);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            double deltaT = gameTime.ElapsedGameTime.TotalSeconds;
            double tempTheta = theta;
            double tempOmega = omega;
            theta += deltaT * tempOmega;
            omega += deltaT * (-1 * mu * tempOmega - 100 * (g / length) * Math.Sin(tempTheta));

            endpoint = origin;
            endpoint.X += (float)(length * Math.Cos(theta + (Math.PI / 2d)));
            endpoint.Y += (float)(length * Math.Sin(theta + (Math.PI / 2d)));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the Line
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                rectangle,
                origin,
                null,
                Color.White,
                (float)theta + (float)(Math.PI / 2d),
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                1f);

            // Draw the pendulum
            _spriteBatch.Draw(
                circleTexture,
                endpoint,
                null,
                Color.White,
                0f,
                new Vector2(circleTexture.Width / 2, circleTexture.Height / 2),
                Vector2.One * 0.5f,
                SpriteEffects.None,
                0f);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}