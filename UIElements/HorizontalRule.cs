using System;

namespace Renderer
{
    public class HorizontalRule : IRenderable
    {
        public Vector2 Position { get; set; }
        
        public int width;
        public BorderStyle borderStyle;

        string previousString;

        public HorizontalRule(int width, BorderStyle borderStyle)
        {
            this.width = width;
            this.borderStyle = borderStyle;
        }

        public void Render()
        {
            string s = new string(new Border(borderStyle).horizontalChar, width);
            previousString = s;
            Console.Write(s);
        }

        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}