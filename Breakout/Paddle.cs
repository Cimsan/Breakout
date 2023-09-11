using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout;

public class Paddle
{
    public Sprite sprite;
    public Vector2f size;
    public Paddle()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("C:\\Users\\Cim\\RiderProjects\\Breakout\\assets\\paddle.png");
        sprite.Position = new Vector2f(250, 750);
        
    }
    
    public void Update(Ball ball, float deltaTime)//Paddle doesn't move or interract with the ball, yet.
    {
        var newPos = sprite.Position;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += deltaTime * 300.0f;
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= deltaTime * 300.0f;
        }
 
        if (Collision.CircleRectangle(
                ball.sprite.Position, Ball.Radius,
                this.sprite.Position, size, out Vector2f hit))
        {
            ball.sprite.Position += hit;
            ball.Reflect(hit.Normalized());
        }

        sprite.Position = newPos;
        
    }
      
        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
            Vector2f paddleTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Origin = 1.0f * paddleTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / paddleTextureSize.Y,
                Diameter / paddleTextureSize.Y);
            
            size = new Vector2f(
                sprite.GetGlobalBounds().Width,
                sprite.GetGlobalBounds().Height
            );
        }

    public const float Diameter = 20.0f;    
    
}