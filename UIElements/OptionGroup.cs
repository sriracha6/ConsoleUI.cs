using System;
using System.Collections.Generic;

namespace Renderer
{
    public class OptionGroup : IInteractive
    {
        public Vector2 Position {get; set;}
        public List<string> Options = new List<string>();
        public int selectedOption;
        
        int hoveredOption = -1;

        string previousString = "";

        ConsoleColor selectedColor = ConsoleColor.DarkGray;

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

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

        public OptionGroup(List<string> options)
        {
            Options = options;
        }

        public OptionGroup(List<string> options, ConsoleColor selectedColor)
        {
            Options = options;
            this.selectedColor = selectedColor;
        }

        public void Render()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                if(selectedOption == i)
                {
                    Console.BackgroundColor = selectedColor;
                    Console.Write(Options[i]+"\n");
                    Console.ResetColor();
                }
                else
                    Console.Write(Options[i]+"\n");
                
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                previousString += UIElement.ParsePreviousString(Options[i]+"\n");
            }
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            int nlcount = 0;
            for(int i = 0; i < previousString.Length; i++)
            {
                if(previousString[i] == ' ')
                    Console.Write(' ');
                else
                {
                    nlcount++;
                    Console.SetCursorPosition(Position.x, Position.y+nlcount);
                }
            }
            previousString = "";
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