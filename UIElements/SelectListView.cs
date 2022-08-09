using System;
using System.Collections.Generic;

namespace Renderer
{
    // TODO: any changes to listview must be made to this. im gonna forget to do it and then have to rewrite this
    public class SelectListView : IInteractive
    {
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        int _Height;

        public int SelectedItemIndex;
        public string SelectedItem { get { return options[SelectedItemIndex]; } }

        int hoveredItem;

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

        public ConsoleColor selectedColor;

        public bool outieScrollbars = false;
        List<string> options = new List<string>();

        int progress;
        public VerticalScrollBar scrollBar;

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        public SelectListView(List<string> options, int width, int height, ConsoleColor selectedColor, bool outieScrollbars)
        {
            this.options = options;
            this.Width = width;
            this._Height = height;
            this.selectedColor = selectedColor;
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
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            if(scrollBar == null)
            {
                scrollBar = new VerticalScrollBar(0, _Height);
                if(!outieScrollbars)
                    scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
                else
                    scrollBar.Position = new Vector2(Position.x + Width, Position.y);
                scrollBar.Render();
            }
            scrollBar.ReRender();

            int y = 0;
            for (int i = progress; i < progress+Height+1; i++)
            {
                if(i >= Height+1+progress || i >= options.Count) 
                    break;
                if(i == SelectedItemIndex)
                    Console.BackgroundColor = selectedColor; 
                Console.SetCursorPosition(Position.x, Position.y + y);
                Console.Write(options[i]);
                Console.ResetColor();
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
        public void OnClick() 
        { 
            SelectedItemIndex = hoveredItem;
            if(OnValueChangeEvent != null)
                OnValueChangeEvent();
            ReRender();
        }
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
        public void OnLeftArrow() 
        {
            if(hoveredItem-1 > -1)
                hoveredItem--;
        }
        public void OnRightArrow() 
        {
            if(hoveredItem+1 < options.Count)
                hoveredItem++;
        }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}