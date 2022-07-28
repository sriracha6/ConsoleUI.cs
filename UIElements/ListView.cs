using System;
using System.Collections.Generic;

namespace Renderer
{
    public class ListView : IInteractive
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        int _Height;
        public int Height 
        {
            get { return _Height; }
            set { _Height = value; 
            
            scrollBar = new VerticalScrollBar(0, _Height);
            scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
            scrollBar.Render();

            ReRender();
            
            }
        }
        List<string> options = new List<string>();

        int progress;
        public VerticalScrollBar scrollBar;

        public ListView(List<string> options, int width, int height)
        {
            this.options = options;
            this.Width = width;
            this._Height = height;
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
            if(scrollBar == null)
            {
                scrollBar = new VerticalScrollBar(0, _Height);
                scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
                scrollBar.Render();
            }
            scrollBar.ReRender();

            int y = 0;
            for (int i = progress; i < progress+Height+1; i++)
            {
                if(i >= Height+1+progress || i >= options.Count) 
                    break;
                Console.SetCursorPosition(Position.x, Position.y + y);
                Console.Write(options[i]);
                y++;
            }
        }

        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.ResetColor();
            for(int i = 0; i < Height+1; i++)
            {
                Console.Write(new string(' ', Width));
                Console.SetCursorPosition(Position.x, Position.y+i+1);
            }
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { }
        public void OnUpArrow() 
        {
            int scrollerHeight = Height-1;
            Console.WriteLine(scrollerHeight);
            if(progress-1 > -1)
            {
                /*if (progress-1 < Height)
                    scrollBar.Progress = 0;
                else if (progress-1 > options.Count - Height)
                    scrollBar.Progress = Height;
                else*/
                scrollBar.Progress = Height-(int)(1f/(progress - 1 + Height/2) * (options.Count)); 
                progress--;
            }
            else
                scrollBar.Progress = 0;
            ReRender();
        }
        public void OnDownArrow() 
        {
            int scrollerHeight = Height-1;
            if(progress+1 < options.Count-Height)
            {
                //for(int i = 0; i < (int)((progress+1)%(options.Count-Height/scrollerHeight)); i++)
                //    scrollBar.OnDownArrow();
                /*if (progress+1 < Height)
                    scrollBar.Progress = 0;
                else if (progress+1 > options.Count - Height)
                    scrollBar.Progress = Height;
                else*/
                scrollBar.Progress = Height-(int)(1f/(progress + 1 + Height/2) * (options.Count)); 
                progress++;
            }
            else
                scrollBar.Progress = Height-1;
            ReRender();
        }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}