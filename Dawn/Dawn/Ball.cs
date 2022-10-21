using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace Dawn
{
    public class Ball
    {



        public Vector2 position = Vector2.Zero;

        public Vector2[] tail = new Vector2[1000];

        public Color color = Color.DarkRed;
        

        public float reverse = 1f;

        private int state = 0;

        public Ball()
        {
            for(int i = 0; i < tail.Count(); i++)
            {
                tail[i] = Vector2.Zero;
            }
        }

        public void Update(GameTime gameTime, Hair[,] hairs)
        {
            state += 1;
            if (position.X > 1000 || position.X < 0 || position.Y < 0 || position.Y > 1000)
            {
                reverse = -reverse;
                //color = Color.Multiply(color, 0.5f);
                Random random = new Random();
                position = tail[(state - 2) % tail.Count()];

            }
            else
            {
                try
                {
                    float a = hairs[(int)(position.X / 20), (int)(position.Y / 20)].rotation;
                    position += reverse * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds * new Vector2((float)Math.Cos(a - Math.PI / 2), (float)Math.Sin(a - Math.PI / 2));

                }
                catch(Exception e)
                {
                    Random random = new Random();
                    if (reverse == 1)
                        position = new Vector2(700 + 100 * (float)random.NextDouble(), 500 * (float)random.NextDouble());
                    if (reverse == -1)
                        position = new Vector2(100 * (float)random.NextDouble(), 500 * (float)random.NextDouble());

                }
            }

            tail[state % tail.Count()] = position;
            
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            float a = 1f / tail.Count();
            float t = 1f;
            spriteBatch.Draw(Hair.Texture, new Rectangle((int)tail[state%tail.Count()].X, (int)tail[state%tail.Count()].Y, 1, 1), Color.Multiply(color, t));
            for (int i = (state-1)%tail.Count(); i > 0; i--)
            {
                spriteBatch.Draw(Hair.Texture, new Rectangle((int)tail[i].X, (int)tail[i].Y, 1, 1), Color.Multiply(color, t));
                t -= a;
            }
            for (int i = tail.Count()-1; i > state%tail.Count(); i--)
            {
                spriteBatch.Draw(Hair.Texture, new Rectangle((int)tail[i].X, (int)tail[i].Y, 1, 1), Color.Multiply(color, t));
                t -= a;
                
            }

        }
    }
}
