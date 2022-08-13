using System;
using System.Threading;

namespace Renderer
{
    public static class Window
    {
        public static int width;
        public static int height;

        public static ConsoleColor SelectedColor;

        public static int AnimationRateMS;

        static void FixWindowSize()
        {
            for(;;)
            {
                if(Console.WindowWidth != width || Console.WindowHeight != height)
                {
                    Console.SetWindowSize(width, height);
                    Console.SetBufferSize(width, height);
                }
            }
        }

        public static void Init(int consoleWidth, int consoleHeight, ConsoleColor selectedColor, int animationRateMS=750)
        {
            Console.CursorVisible = false;
            width = consoleWidth;
            height = consoleHeight;
            SelectedColor = selectedColor;
            AnimationRateMS = animationRateMS;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            Thread inputThread = new Thread(Input.GetInput);
            inputThread.Start();
            Thread windowSizeThread = new Thread(FixWindowSize);
            windowSizeThread.Start();
            Thread animationThread = new Thread(Animate);
            animationThread.Start();
        }

        private static void Animate()
        {
            for(;;)
            {
                Thread.Sleep(AnimationRateMS);
                foreach(IAnimatable r in Renderer.animatableItems) 
                    if(r.Visible)
                        r.Tick();
            }
        }
    }
}