using System;
using System.Collections.Generic;

namespace Renderer
{
    public class ListView : IRenderable
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BorderType borderType;

        List<string> options = new List<string>();

        public VerticalScrollBar scrollBar;

        public ListView(List<string> options, int width, int height, BorderType BorderType)
        {
            this.options = options;
            this.Width = width;
            this.Height = height;
            this.borderType = BorderType;
        }

        public void AddItem(string item)
        {
            options.Add(item);
            ReRender();
        }

        public void RemoveItem(string item)
        {
            options.Remove(item);
            ReRender();
        }

        public void Render()
        {
            Panel p = new Panel(Width, Height, borderType, ' ');
            p.Position = Position;
            p.Render();
            Console.SetCursorPosition(Position.x, Position.y);
            for (int i = 0; i < options.Count; i++)
            {
                Console.Write(options[i]);
                Console.SetCursorPosition(Position.x, Position.y + i + 1);
            }
        }

        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(new string(' ', Width * Height));
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}