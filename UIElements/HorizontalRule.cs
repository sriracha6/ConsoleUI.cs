using System;

namespace Renderer
{
    public class HorizontalRule : IRenderable
    {
        public Vector2 Position { get; set; }
        
        public int width;
        public BorderType borderStyle;

        string previousString;
        
        bool _Visible;
        public bool Visible 
        { 
            get { return _Visible; } 
            set 
            {
                _Visible = value;
                if (value)
                    Render();
                else
                    DeRender();
            } 
        }

        public HorizontalRule(int width, BorderType borderStyle)
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

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}