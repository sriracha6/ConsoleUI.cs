using System;
using System.Linq;
using System.Collections.Generic;

namespace Renderer
{
    public class OptionGroup : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        List<string> Options = new List<string>();
        public int selectedOption;
        
        int hoveredOption = -1;

        int previousStringH;
        int previousStringW;

        ConsoleColor _selectedColor = ConsoleColor.DarkGray;
        ConsoleColor selectedColor { get { return _selectedColor; } set {_selectedColor = value; if(_Position != null) ReRender(); } }

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

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

        public OptionGroup(List<string> options)
        {
            Options = options;
        }

        public OptionGroup(List<string> options, ConsoleColor selectedColor)
        {
            Options = options;
            this.selectedColor = selectedColor;
        }

        public void AddOption(string op)
        {
            Options.Add(op);
        }

        public void RemoveOption(string op)
        {
            Options.Remove(op);
        }

        public void ClearOptions(string op)
        {
            Options.Clear();
        }

        public void Render()
        {
            if(Selected)
            { 
                Console.BackgroundColor = Window.SelectedColor;
/*
                for(int i = 0; i < Options.Count; i++)
                {
                    for(int j = 0; j < Options.OrderBy(x=>x.Length).First().Length; j++)
                    {
                        Console.Write(' ');
                    }
                }
                Console.SetCursorPosition(Position.x, Position.y);*/
            }
            for (int i = 0; i < Options.Count; i++)
            {
                if(selectedOption == i)
                {
                    Console.BackgroundColor = selectedColor;
                    Console.Write(Options[i]+"\n");
                    if(!Selected) Console.ResetColor();
                    else Console.BackgroundColor = Window.SelectedColor;
                }
                else
                    Console.Write(Options[i]+"\n");
                
                Console.SetCursorPosition(Position.x, Position.y+i+1);
            }
            previousStringW = Options.OrderBy(x=>x.Length).First().Length;
            previousStringH = Options.Count;
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Console.ResetColor();
            int nlcount = 0;
            for(int i = 0; i < previousStringH; i++)
            {
                Console.Write(new string(' ', previousStringW));
                nlcount++;
                Console.SetCursorPosition(Position.x, Position.y+nlcount);
            }
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { }
        public void OnUpArrow() 
        { 
            if(hoveredOption-1 >= 0)
            {
                hoveredOption--;
                selectedOption--;
                selectedOption = hoveredOption;
                if(OnValueChangeEvent!=null) OnValueChangeEvent(); 
                ReRender();
            }
        }
        public void OnDownArrow() 
        { 
            if (hoveredOption+1 < Options.Count)
            {
                hoveredOption++;
                selectedOption++;
                selectedOption = hoveredOption;
                if(OnValueChangeEvent!=null) OnValueChangeEvent(); 
                ReRender();
            }
        }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}