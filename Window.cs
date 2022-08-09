using System;
using System.Threading;

namespace Renderer
{
    public static class Window
    {
        public static int width;
        public static int height;

        public static ConsoleColor SelectedColor;

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

        public static void Init(int consoleWidth, int consoleHeight, ConsoleColor selectedColor)
        {
            Console.CursorVisible = false;
            width = consoleWidth;
            height = consoleHeight;
            SelectedColor = selectedColor;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            Thread inputThread = new Thread(Input.GetInput);
            inputThread.Start();
            Thread windowSizeThread = new Thread(FixWindowSize);
            windowSizeThread.Start();
        }
    }
}