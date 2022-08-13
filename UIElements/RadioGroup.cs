using System;
using System.Linq;
using System.Collections.Generic;

namespace Renderer
{
    public class RadioGroup : IInteractive
    {
        public Vector2 Position {get; set;}
        public List<string> Options = new List<string>();
        public int selectedOption;
        
        int hoveredOption = -1;

        string leftSide = "[";
        string rightSide = "]";
        string selected = ">";

        string previousString = "";

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

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        public RadioGroup(List<string> options)
        {
            Options = options;
        }

        public RadioGroup(List<string> options, string leftside, string rightside, string selected)
        {
            Options = options;
            this.selected = selected;
            this.rightSide = rightside;
            this.leftSide = leftside;
        }

        public void Render()
        {
            if(Selected)
            { 
                Console.BackgroundColor = Window.SelectedColor;

                for(int i = 0; i < Options.Count; i++)
                {
                    for(int j = 0; j < Options.OrderBy(x=>x.Length).First().Length; j++)
                    {
                        Console.Write(' ');
                    }
                }
                Console.SetCursorPosition(Position.x, Position.y);
            }
            for (int i = 0; i < Options.Count; i++)
            {
                string s = leftSide + (selectedOption == i ? selected : " ") + rightSide + " " + Options[i];
                Console.Write(s+"\n");
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                previousString += UIElement.ParsePreviousString(s+"\n");
            }
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
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
        public void OnClick() 
        { 
            selectedOption = hoveredOption;
            if(OnValueChangeEvent!=null) OnValueChangeEvent(); 
            ReRender();
        }
        public void OnUpArrow() 
        { 
            if(hoveredOption-1 >= 0)
            {
                hoveredOption--;
                selectedOption--;
            }
        }
        public void OnDownArrow() 
        { 
            if (hoveredOption+1 < Options.Count)
            {
                hoveredOption++;
                selectedOption++;
            }
        }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}