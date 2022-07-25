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
    }
}