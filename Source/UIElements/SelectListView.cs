using System;
using System.Collections.Generic;
using System.Linq;

namespace Renderer
{
    // TODO: any changes to listview must be made to this. im gonna forget to do it and then have to rewrite this
    public class SelectListView : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        int _Height;
        int _Width;
        public int Width { get { return _Width; } set { _Width = value; if(_Position != null) ReRender(); } }
        int _selecteditemindex;
        public int SelectedItemIndex { get { return _selecteditemindex; } set { _selecteditemindex = value; if(_Position != null) ReRender(); } }
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

        ConsoleColor _slc;
        public ConsoleColor selectedColor { get { return _slc; } set { _slc = value; if(_Position != null) ReRender(); } }

        bool _outieScrollbars;
        public bool outieScrollbars { get { return _outieScrollbars; } set { _outieScrollbars = value; ReRender(); } }
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
            if(scrollBar == null)
            {
                scrollBar = new VerticalScrollBar(0, _Height);
                if(!outieScrollbars)
                    scrollBar.Position = new Vector2(Position.x + Width - 1, Position.y);
                else
                    scrollBar.Position = new Vector2(Position.x + Width, Position.y);
            }
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
                if(i == SelectedItemIndex)
                    Console.BackgroundColor = selectedColor; 
                Console.SetCursorPosition(Position.x, Position.y + y);
                Console.Write(options[i]);

                if(Selected) Console.BackgroundColor = Window.SelectedColor;
                else Console.ResetColor();

                if(i == progress+Height) Console.Write(new string(' ', Width-options[i].Length));
                //Console.ResetColor();
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
            if(Position == null) return;
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
            if(hoveredItem-1 > -1)
                hoveredItem--;
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
            if(hoveredItem+1 < options.Count)
                hoveredItem++;
            ReRender();
        }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }

        public void OnHoverLeave() {  }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}