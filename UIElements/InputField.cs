using System;

namespace Renderer
{
    public class InputField : IInteractive
    {
        public Vector2 Position {get; set;}
        public string text;
        public int width;
        public int maxTextLength;

        int scrollLeft;

        public string leftSide = "[";
        public string rightSide = "]";

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

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        string previousString;

        public InputField(string text, int width, int maxTextLength)
        {
            this.text = text;
            this.width = width;
            this.maxTextLength = maxTextLength;
        }

        public InputField(string text, int width, int maxTextLength, string leftside, string rightside)
        {
            this.text = text;
            this.width = width;
            this.maxTextLength = maxTextLength;
            this.leftSide = leftside;
            this.rightSide = rightside;
        }

        public void Render()
        {
            int amount = width;
            if(width+scrollLeft > text.Length)
            {
                amount = text.Length-scrollLeft;
            }
            string s = leftSide + text.Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide;
            Console.Write(s);
            previousString = UIElement.ParsePreviousString(s);
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() 
        {
            if (scrollLeft > 0)
            {
                scrollLeft--;
                ReRender();
            }
        }
        public void OnRightArrow() 
        {
            if (scrollLeft < text.Length - width)
            {
                scrollLeft++;
                ReRender();
            }
        }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) 
        {
            if(text.Length+1 > maxTextLength) return;
            if(character.Key == ConsoleKey.Backspace && text.Length-1 >= 0)
            {
                if(text.Length == 1)
                    text = "";
                else
                    text = text.Remove(text.Length-1, 1);
                OnLeftArrow();
            }
            else
            {
                text += character.KeyChar;
                OnRightArrow();
            }
            if(OnValueChangeEvent != null) OnValueChangeEvent();
            ReRender();
        }
    }
}