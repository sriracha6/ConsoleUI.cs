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

        public const string START_BOLD = "\x1b[1m";
        public const string END_BOLD = "\x1b[0m";
    }
}