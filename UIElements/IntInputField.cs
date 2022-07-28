using System;

namespace Renderer
{
    public class IntInputField : IInteractive
    {
        public Vector2 Position {get; set;}
        public int text;
        public int width;
        public int maxValue;
        public int minValue;

        int scrollLeft;

        public string leftSide = "[";
        public string rightSide = "]";
        public string upDown = "^v";

        bool _Visible;
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

        string previousString;

        public IntInputField(int text, int width, int maxValue, int minValue)
        {
            this.text = text;
            this.width = width;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public IntInputField(int text, int width, int maxValue, int minValue, string leftside, string rightside, string upDown)
        {
            this.text = text;
            this.width = width;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.leftSide = leftside;
            this.rightSide = rightside;
            this.upDown = upDown;
        }

        public void Render()
        {
            int amount = width;
            if(width+scrollLeft > text.ToString().Length)
            {
                amount = text.ToString().Length-scrollLeft;
            }
            string s = leftSide + text.ToString().Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide + upDown;
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

        void Clamp() { if(text > maxValue) text = maxValue;
            if(text < minValue) text = minValue;}
        public void OnHover() { }
        public void OnClick() { }
        public void OnUpArrow() { text++; Clamp(); ReRender(); }
        public void OnDownArrow() { text--; Clamp(); ReRender(); }
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
            if (scrollLeft < text.ToString().Length - width)
            {
                scrollLeft++;
                ReRender();
            }
        }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) 
        {
            if(character.Key == ConsoleKey.Backspace && text >= 0)
            {
                if(text.ToString().Length == 1)
                    text = 0;
                else
                    text = int.Parse(text.ToString().Remove(text.ToString().Length-1, 1));
                OnLeftArrow();
            }
            else
            {
                int a;
                if(int.TryParse(character.KeyChar.ToString(), out a))
                {
                    text = int.Parse((text.ToString() + character.KeyChar.ToString()));
                    OnRightArrow();
                }
                else if(character.Key == ConsoleKey.OemMinus)
                {
                    text = -text;
                }
            }
            Clamp();
            ReRender();
        }
    }
}