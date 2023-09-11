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
        Ball ball = new Ball();
        Paddle paddle = new Paddle();
      
        public void Run()
        {
            
            Program.ScreenW = windowWidth;        
            Program.ScreenH = windowHeight;
            
            while (window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                ball.Update(deltaTime);
                window.Clear(new Color(80, 80, 200, 200));
                ball.Draw(window);
                paddle.Draw(window);
                window.Display();
                
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
            }
        }
    }
}