using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Dawn
{
    public class Game1 : Game
    {

        int iii = 0;
        int screentart = 30;
        int screenend = 30;

        Stream stream;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        RenderTarget2D renderTarget;

        Hair[,] hairs;

        Ball[] balls;

        KeyboardState key = new KeyboardState();
        KeyboardState previous = new KeyboardState();

        public float t = 0f;

        PenumbraComponent Penumbra;

        public bool toggle = true;
        bool te = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Penumbra = new PenumbraComponent(this);
            
        }

        protected override void Initialize()
        {
            Random r = new Random();
            t = (float)r.NextDouble();
            renderTarget = new RenderTarget2D(GraphicsDevice, 1600, 1000);

            Wallpaper.SET(1);

            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.ApplyChanges();


            balls = new Ball[200];
            Random random = new Random();

            for (int i = 0; i < balls.Count(); i++)
            {
                balls[i] = new Ball();
                balls[i].position = new Vector2(800 * (float)random.NextDouble(), 900 * (float)random.NextDouble());
            }
            /*for (int i = balls.Count() / 2; i < balls.Count(); i++)
            {
                balls[i] = new Ball();
                balls[i].position = new Vector2(100 * (float)random.NextDouble(), 500 * (float)random.NextDouble());
                balls[i].color = Color.Purple;
                balls[i].reverse = -1;
            }*/





            Penumbra.Initialize();

            Hair.InitiateTexture(GraphicsDevice, 5, 20);
            hairs = new Hair[50, 50];
            for(int i = 0; i < 50; i++)
                for(int j = 0; j<50;j++)
                {
                    hairs[i,j] = new Hair();
                    hairs[i,j].vector = new Vector2(i * 20, 2*j* 20);
                    hairs[i, j].color = Color.Purple;
                    hairs[i, j].rotation = (float)Math.Atan2(2*i*2*j, 2*j*(10-2*j)*i);

                    hairs[i,j].light = new PointLight()
                    {
                        Position = hairs[i,j].vector,
                        Color = hairs[i,j].color,
                        Radius = 1,
                        Scale = 10 * Vector2.One,
                    };
                Penumbra.Lights.Add(hairs[i,j].light);
            }

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);



        }

        protected override void Update(GameTime gameTime)
        {

            key = Keyboard.GetState();

            if (previous.IsKeyUp(Keys.B) && key.IsKeyDown(Keys.B))
            {
                balls = new Ball[800];
                Random random = new Random();
                Random r = new Random();
                t = (float)r.NextDouble();

                for (int i = 0; i < balls.Count(); i++)
                {
                    balls[i] = new Ball();
                    balls[i].position = new Vector2(800 * (float)random.NextDouble(), 900 * (float)random.NextDouble());
                }
                /*for (int i = balls.Count() / 2; i < balls.Count(); i++)
                {
                    balls[i] = new Ball();
                    balls[i].position = new Vector2(100 * (float)random.NextDouble(), 500 * (float)random.NextDouble());
                    balls[i].color = Color.Purple;
                    balls[i].reverse = -1;
                }*/
            }


                if (previous.IsKeyUp(Keys.A) && key.IsKeyDown(Keys.A))
            {
                te = !te;

                GraphicsDevice.Clear(Color.Black);
            }

            for (int i = 0; i < 50; i++)
                for (int j = 0; j < 50; j++)
                {

                    hairs[i, j].rotation = -4 * (float)Perlin.OctavePerlin(0.001*(i+t), 0.001*(j+t) + 1, 1, 8, 1);

                }
            t += 0*(float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < balls.Count(); i++)
            {
                balls[i].Update(gameTime, hairs);
            }


            previous = Keyboard.GetState();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            render(gameTime);

             GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(renderTarget, Vector2.Zero, Color.Wheat);

            _spriteBatch.End();


            save();
            Wallpaper.SET(iii);

            base.Draw(gameTime);
        }

        public async void save()
        {
            if(iii > screentart && iii < screenend)
            {
                if (!File.Exists(iii.ToString() + ".png"))
                {
                    Debug.WriteLine(renderTarget.Bounds);
                    stream = File.Create(iii.ToString() + ".png");

                    await op();
                    stream.Dispose();
                }
                iii++;
            }
        }

        private Task op()
        {
            return new Task(() =>
            renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height)
            );
        }

        public void render(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            if (te)
            {
                foreach (Hair hair in hairs)
                    hair.Draw(_spriteBatch);
            }

            for (int i = 0; i < balls.Count(); i++)
            {
                balls[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();



            GraphicsDevice.SetRenderTarget(null);

        }

    }
}
