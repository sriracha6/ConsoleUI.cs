using System;

namespace Renderer
{
    public class Image : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
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

        Pixel[,] _pixels;
        public Pixel[,] Pixels { get { return _pixels; } set { _pixels = value; if(_Position != null) ReRender(); } }

        public int Width { get { return _pixels.GetLength(0); } } 
        public int Height { get { return _pixels.GetLength(1); } }

        int previousStringX;
        int previousStringY;

        public Image(Pixel[,] pixels, int width, int height)
        {
            this.Pixels = pixels;
        }

        public Image(string[] lines, ConsoleColor color, int width, int height)
        {
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
                    UIElement.CursorPos((int)Position.x+i, (int)Position.y+j);
                    Console.ForegroundColor = Pixels[i, j].Color;
                    UIElement.Write(Pixels[i, j].Char);
                }
            }
            previousStringX = Width;//(Width, Height);
            previousStringY = Height;
        }

        public void DeRender()
        {
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Console.ResetColor();
            for(int i = 0; i < previousStringX; i++)
            {
                for(int j = 0; j < previousStringY; j++)
                {
                    UIElement.Write(' ');
                }
            }
            previousStringX = 0; previousStringY = 0;
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Render();
        }
    }
}