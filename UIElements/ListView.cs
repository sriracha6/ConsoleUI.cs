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
            DeRender();
            scrollBar = new VerticalScrollBar(0, _Height);
            scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
            scrollBar.Render();

            ReRender();
            
            }
        }

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

        public bool outieScrollbars = false;
        List<string> options = new List<string>();

        int progress;
        public VerticalScrollBar scrollBar;

        public ListView(List<string> options, int width, int height, bool outieScrollbars)
        {
            this.options = options;
            this.Width = width;
            this._Height = height;
            this.outieScrollbars = outieScrollbars;
        }

        public void AddItem(string item, bool reRender=true)
        {
            options.Add(item);
            if(reRender)
            ReRender();
        }

        public void RemoveItem(string item, bool reRender=true)
        {
            options.Remove(item);
            if(reRender)
            ReRender();
        }

        public void ClearItems(bool reRender=true)
        {
            options.Clear();
            if(reRender)
            ReRender();
        }

        public void Render()
        {
            scrollBar = new VerticalScrollBar(0, _Height);
            if(!outieScrollbars)
                scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
            else
                scrollBar.Position = new Vector2(Position.x + Width, Position.y);
            
            scrollBar.ReRender();
            
            if(Selected)
            {
                Console.BackgroundColor = Window.SelectedColor;
                Console.SetCursorPosition(Position.x, Position.y);
                for(int i = 0; i < Height+1; i++)
                {
                    Console.Write(new string(' ', Width));
                    Console.SetCursorPosition(Position.x, Position.y + i);
                }
            }

            int y = 0;
            for (int i = progress; i < progress+Height+1; i++)
            {
                if(i >= Height+1+progress || i >= options.Count) 
                    break;
                Console.SetCursorPosition(Position.x, Position.y + y);
                Console.Write(options[i]);
                y++;
            }
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.ResetColor();
            for(int i = 0; i < Height+1; i++)
            {
                Console.Write(new string(' ', Width));
                Console.SetCursorPosition(Position.x, Position.y+i+1);
            }
        }

        public void ReRender()
        {
            DeRender();
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