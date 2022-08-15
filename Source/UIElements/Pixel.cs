using System;

namespace Renderer
{
    public class Pixel : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        public char? Char;
        public ConsoleColor Color;

        bool _Visible = true;
        public bool Selected { get; set; }
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

        public Pixel(char? Char, ConsoleColor fgColor)
        {
            this.Char = Char;
            this.Color = fgColor;
        }

        public Pixel(ConsoleColor bgColor)
        {
            this.Color = bgColor;
        }

        public void Render()
        {
            if(Char == null)
            {
                Console.BackgroundColor = Color;
                UIElement.Write(' ');
            }
            else
            {
                Console.ForegroundColor = Color;
                UIElement.Write(Char);
            }
            Console.ResetColor();
        }

        public void DeRender()
        {
            UIElement.CursorPos(Position.x, Position.y);
            Console.ResetColor();
            UIElement.Write(' ');
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos(Position.x, Position.y);
            Render();
        }
    }
}