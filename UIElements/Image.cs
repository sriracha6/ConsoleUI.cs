using System;

namespace Renderer
{
    public class Image : IRenderable
    {
        public Vector2 Position { get; set; }

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

        public Pixel[,] Pixels;
        public int Width;
        public int Height;

        (int x, int y) previousString;

        public Image(Pixel[,] pixels, int width, int height)
        {
            this.Pixels = pixels;
            this.Width = width;
            this.Height = height;
        }

        public Image(string[] lines, ConsoleColor color, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Pixels = new Pixel[width, height];
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    this.Pixels[i, j] = new Pixel(lines[j][i], color);
                }
            }
        }

        public void Render()
        {
            for(int i = 0; i < Width; i++)
            {
                for(int j = 0; j < Height; j++)
                {
                    Console.SetCursorPosition((int)Position.x+i, (int)Position.y+j);
                    Console.ForegroundColor = Pixels[i, j].Color;
                    Console.Write(Pixels[i, j].Character);
                }
            }
            previousString = (Width, Height);
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Console.ResetColor();
            for(int i = 0; i < previousString.x; i++)
            {
                for(int j = 0; j < previousString.y; j++)
                {
                    Console.Write(' ');
                }
            }
            previousString = (0, 0);
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }
    }
}