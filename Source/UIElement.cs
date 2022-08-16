using System;

namespace Renderer
{
    public class UIElement
    {
        public static string ParsePreviousString(string s)
        {
            string asd = "";
            foreach(char c in s)
                if(c == '\n')
                    asd += "\n";
                else
                    asd += " ";
            return asd;
        }

        public static int PercentWidth(int percent)
        {
            return (percent/100) * Console.WindowWidth; 
        }

        public static int PercentHeight(int percent)
        {
            return (percent/100) * Console.WindowHeight; 
        }
        static object lck = new object();
        public static void CursorPos(int x, int y)
        {
            lock(lck) { Console.SetCursorPosition(x, y); }
        }
        public static void Write(object text)
        {
            lock(lck) { Console.Write(text.ToString()); }
        }
        public static void WriteLine(object text)
        {
            lock(lck) { Console.WriteLine(text.ToString()); }
        }

        public static void ClearArea(int x, int y, int width, int height)
        {
            lock(lck) 
            {
                Console.ResetColor();
                Console.SetCursorPosition(x,y);
                for(int i = 0; i < height; i++)
                {
                    Console.Write(new string(' ', width));
                    Console.SetCursorPosition(x,y+i);
                }               
            }
        }
    }
}