using System;

namespace Renderer
{
    public class Pixel : IRenderable
    {
        public Vector2 Position { get; set; }

        public char Char;
        public ConsoleColor Color;

        public Pixel(char Char, ConsoleColor fgColor)
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

        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.ResetColor();
            Console.Write(' ');
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}