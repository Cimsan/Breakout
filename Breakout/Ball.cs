using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class Ball
    {

        public Sprite sprite;

        public Ball()
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("C:\\Users\\Cim\\RiderProjects\\Breakout\\assets\\ball.png");
            sprite.Position = new Vector2f(250, 400);
        }
       
        public void Update(float deltaTime)
        {
            
            var newPos = sprite.Position;
            
            newPos += direction * deltaTime * ballSpeed;
            if (newPos.X > Program.ScreenW - Radius || newPos.X < 0 + Radius)
            {                
                Reflect(new Vector2f(-1, 0));
            }

            if (newPos.Y > Program.ScreenH - Radius || newPos.Y < 0 + Radius)
            {               
                Reflect(new Vector2f(0, -1));
            }
            sprite.Position = newPos;
        }
        
        public Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        
        public void Reflect(Vector2f normal)
        {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
            Vector2f ballTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / ballTextureSize.X,
                Diameter / ballTextureSize.Y);
        }

        public const float Diameter = 20.0f;
        public const float Radius = Diameter * 0.5f;
        public float ballSpeed = 500.0f;

    }
}