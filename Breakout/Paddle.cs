using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace breakout;

public class Paddle
{
    public Sprite sprite;
    public Vector2f size;
    public const float Diameter = 20.0f;
    public const float Radius = Diameter * 0.5f;
    public Paddle()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets\\paddle.png");
        sprite.Position = new Vector2f(280, 750);
        
    }
    
    public void Update(Ball ball, float deltaTime)
    {
        var newPos = sprite.Position;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
        {
            newPos.X += deltaTime * 400.0f;
            if (newPos.X > Program.ScreenW)
            {
                newPos.X = Program.ScreenW;// - size.X / 2;
            }
        }

        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
        {
            newPos.X -= deltaTime * 400.0f;
            if (newPos.X < size.X)
            {
                newPos.X = size.X; 
            }
        }
 
        if (Collision.CircleRectangle(
                ball.sprite.Position, Ball.Radius,
                new Vector2f(this.sprite.Position.X - size.X / 2, this.sprite.Position.Y - size.Y / 2), size, out Vector2f hit))
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
}