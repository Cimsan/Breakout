using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace breakout
{
    public class Window
    {
        static uint windowWidth = 500;
        static uint windowHeight = 800;

        RenderWindow window = new(new VideoMode(windowWidth, windowHeight), "breakout");

        Clock clock = new Clock();
        Ball ball = new Ball(new Vector2f(250, 400), new Vector2f(0, 1));

        Paddle paddle = new Paddle();

        Tiles tiles = new Tiles();
      
        public void Run()
        {
            
            Program.ScreenW = windowWidth;        
            Program.ScreenH = windowHeight;
            
            while (window.IsOpen)
            {                
                if (ball.health > 0)
                {
                float deltaTime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                window.Clear(Color.Blue);
                ball.Update(deltaTime, paddle.sprite.Position.X - paddle.size.X/2);
                paddle.Update(ball,deltaTime);
                tiles.Update(ball, deltaTime);
                ball.Draw(window);
                paddle.Draw(window);
                tiles.Draw(window);
                window.Display();
                }
                else
                {
                    //gameovercode
                    ball.score = 0;
                    ball.health = 3;
                    tiles.Reset();
                    ball.ResetBall(new Vector2f(250, 720));
                }
                
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
            }
        }
    }
}