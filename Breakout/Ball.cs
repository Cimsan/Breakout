using System.Security.Cryptography.X509Certificates;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class Ball
    {
        Text gui = new Text();
        public int health = 3;
        public int score = 0;
        public const float Diameter = 20.0f;
        public const float Radius = Diameter * 0.5f;
        public float ballSpeed = 500.0f;
        public Vector2f direction;
        public Sprite sprite;
        private bool resetKeyPressed = true;

        public Ball(Vector2f position, Vector2f direction)
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets\\ball.png");
            sprite.Position = position;
            this.direction = direction;
            ResetBall(new Vector2f(250, 720));
            
            gui.Font = new Font("assets\\future.ttf");
        }

        public void ResetBall(Vector2f position)
        {
            sprite.Position = position;
            direction = new Vector2f(0, 0);
            resetKeyPressed = false;
        }
       
        public void Update(float deltaTime, float paddelPos)
        {
            
            var newPos = sprite.Position;
            
            newPos += direction * deltaTime * ballSpeed;
            if (newPos.X > Program.ScreenW - Radius || newPos.X < 0 + Radius)
            {                
                Reflect(new Vector2f(-1, 0));
            }
            if (resetKeyPressed == false)
            {
                newPos.X = paddelPos;
            }
            sprite.Position = newPos;
            if (newPos.Y > Program.ScreenH - Radius || newPos.Y < 0 + Radius)
            {
                if (newPos.Y > Program.ScreenH - Radius)
                {
                    health--;
                    ResetBall(new Vector2f(250, 720));
                }
                Reflect(new Vector2f(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && resetKeyPressed == false)
            {
                Random random = new Random();
                float x, y = 0.0f;
                while (y <= 50.0f && y >= -50.0f)
                {
                    y = random.Next(-100, 100);
                }

                x = random.Next(-100, 100);
                float length = MathF.Sqrt(x * x + y * y);
                y /= length;
                x /= length;
                direction = new Vector2f(x, y); // random direction
                resetKeyPressed = true;
            }
        }

        public void Reflect(Vector2f normal)
        {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
        }

        public void Draw(RenderTarget target)
        {
            gui.CharacterSize = 24;
            target.Draw(sprite);
            gui.DisplayedString = $"Health: {health}";
            gui.Position = new Vector2f(12, 8);
            target.Draw(gui);
            gui.DisplayedString = $"Score: {score}";
            gui.Position = new Vector2f(Program.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
            target.Draw(gui);
            Vector2f ballTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / ballTextureSize.X,
                Diameter / ballTextureSize.Y);
        }
    }
}