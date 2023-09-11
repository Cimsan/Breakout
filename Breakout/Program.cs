using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    class Program
    {
        
        public static float ScreenW { get; set; }
        public static float ScreenH { get; set; }
        
        static void Main(string[] args)
        {
            Window window = new Window();
            window.Run();
        }
            
    }
}
