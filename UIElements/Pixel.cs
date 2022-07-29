using System;

namespace Renderer
{
    public class Pixel : IRenderable
    {
        public Vector2 Position { get; set; }

        public char? Char;
        public ConsoleColor Color;

        bool _Visible = true;
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
                Console.Write(' ');
            }
            else
            {
                Console.ForegroundColor = Color;
                Console.Write(Char);
            }
            Console.ResetColor();
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.ResetColor();
            Console.Write(' ');
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}