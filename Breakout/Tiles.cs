using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace breakout;

public class Tiles
{
    public Sprite sprite;
    public Vector2f size;
    List<Vector2f> positions = new List<Vector2f>();
   
    public Tiles()
    {
        sprite = new Sprite();
        sprite.Texture = new Texture("assets\\tileBlue.png");
        
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                var pos = new Vector2f(
                    500 * 0.5f + i * 188 * 0.5f + (188/4),
                    800 * 0.3f + j * 88 * 0.5f + (88/4));
                positions.Add(pos);
            }
        }
    }

    public void Reset()
    {
        positions.Clear();
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                var pos = new Vector2f(
                    500 * 0.5f + i * 188 * 0.5f + (188/4),
                    800 * 0.3f + j * 88 * 0.5f + (88/4));
                positions.Add(pos);
            }
        }
    }

    public void Draw(RenderTarget target)
        {
            Vector2f tilesTextureSize = (Vector2f) sprite.Texture.Size;
            sprite.Origin = 1.0f * tilesTextureSize;
            sprite.Scale = new Vector2f(
                0.5f,
                0.5f);
            size = new Vector2f(
                sprite.GetGlobalBounds().Width ,
                sprite.GetGlobalBounds().Height
            );
            for (int i = 0; i < positions.Count; i++)
            {
                sprite.Position = positions[i];
                target.Draw(sprite);
            }
        }

    public void Update(Ball ball, float deltaTime)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2f pos = positions[i];
            if (Collision.CircleRectangle(
                    ball.sprite.Position, Ball.Radius,
                    new Vector2f(pos.X - size.X/2, pos.Y - size.Y/2), size, out Vector2f hit))
            {
                ball.sprite.Position += hit;
                ball.Reflect(hit.Normalized());
                positions.RemoveAt(i);
                ball.score += 100;
                i = 0; // Check all again since ball was moved
            }
        }

        if (positions.Count <= 0)
        {
            Reset();
            ball.ResetBall(new Vector2f(250, 720));
        }
    }
}