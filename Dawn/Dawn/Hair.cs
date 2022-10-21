using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;

namespace Dawn
{
    public class Hair
    {

        public static Texture2D Texture;
        public static Texture2D Texture2;

        public Color color = Color.White;
        public float rotation = 0f;
        public Vector2 vector = new Vector2(0, 0);


        public Light light
        {
            get
            {
                return new PointLight()
                {
                    Position = lightPos,
                    Scale = scale * Vector2.One,
                    Color = lightColor,
                };
            }
            set
            {
                lightPos = value.Position;
                scale = (float)Vector2.Distance(Vector2.Zero, value.Scale);
                lightColor = value.Color;
            }
        }


        private Vector2 lightPos = Vector2.Zero;
        private float scale = 1f;
        private Color lightColor = Color.White;
        

        public static void InitiateTexture(GraphicsDevice device, int width, int height)
        {
            Texture = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];

            for(int i = 0; i < data.Count();i++)
            {
                data[i] = Color.White;
            }

            Texture.SetData(data);

        }

        public Hair()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, vector, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, rotation, Vector2.Zero, 1,  SpriteEffects.None, 0f);
        }

    }
}
