using System;
using System.Collections.Generic;

namespace Renderer
{
    public class DropdownMenu : IInteractive
    {
        public Vector2 Position {get; set;}
        public int SelectedIndex;
        List<string> options;
        public string SelectedItem { get { return options[SelectedIndex]; } }
        public int Width;
        public int Height;

        string leftSide = "[";
        string rightSide = "]";
        string dropdown = "[v]";

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

        int previousString;

        public ConsoleColor SelectedColor;
        SelectListView slv;

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        public DropdownMenu(List<string> options, int width, int height, ConsoleColor selectedColor)
        {
            this.options = options;
            this.Width = width;
            this.Height = height;
            this.SelectedColor = selectedColor;
            slv = new SelectListView(options, width,height,selectedColor,true);
        }

        public DropdownMenu(List<string> options, int width, int height, ConsoleColor selectedColor, string leftSide, string rightSide, string dropDown)
        {
            this.options = options;
            this.Width = width;
            this.Height = height;
            this.SelectedColor = selectedColor;
            slv = new SelectListView(options, width,height,selectedColor,true);
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.dropdown = dropDown;
        }

        public void AddItem(string item, bool reRender=true)
        {
            options.Add(item);
            slv.AddItem(item, reRender);
            if(reRender)
            ReRender();
        }

        public void RemoveItem(string item, bool reRender=true)
        {
            options.Remove(item);
            slv.RemoveItem(item, reRender);
            if(reRender)
            ReRender();
        }

        public void ClearItems(bool reRender=true)
        {
            options.Clear();
            slv.ClearItems(reRender);
            if(reRender)
            ReRender();
        }

        public void Render()
        {
            if(slv.Position == null)
            {
                slv.Position = new Vector2(Position.x, Position.y+1);
                slv.Visible = false;
            }
            string s = leftSide + " " + SelectedItem + " " + dropdown + rightSide;
            if(SelectedItem.Length > Width)
                s = s.Replace(SelectedItem, SelectedItem.Substring(0, Width-5) + "...");
            else
                s = s.PadRight(Width);
            
            previousString = s.Length;
            Console.Write(s);
            if(slv.Visible)
                slv.ReRender();
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(new string(' ', previousString));
            if(slv.Visible) slv.Visible = false;
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
            if(!slv.Visible)
                slv.Visible = true;
            else
            {
                slv.OnClick();
                slv.Visible = false;
                SelectedIndex = slv.SelectedItemIndex;
                if(OnValueChangeEvent != null)
                    OnValueChangeEvent();
            }
            ReRender();
        }
        public void OnUpArrow() { if(slv.Visible) slv.OnUpArrow(); }
        public void OnDownArrow() { if(slv.Visible) slv.OnDownArrow(); }
        public void OnLeftArrow() { if(slv.Visible) slv.OnLeftArrow(); }
        public void OnRightArrow() { if(slv.Visible) slv.OnRightArrow(); }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { slv.OnTextInput(character); }
    }
}