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

        public void ReRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            for(int i = 0; i < previousString.Length; i++)
            {
                if(previousString[i] != '\n')
                    Console.Write(' ');
                else
                    Console.SetCursorPosition((int)Position.x, (int)Position.y+i+1);
            }
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