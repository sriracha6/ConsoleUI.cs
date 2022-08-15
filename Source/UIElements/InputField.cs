using System;

namespace Renderer
{
    public class InputField : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        
        string _text;
        public string text { get { return _text; } set { _text = value; if(_Position != null) ReRender(); } }
        int _width;
        public int width { get { return _width; } set { _width = value; if(_Position != null) ReRender(); } } 
        int _maxtext;
        public int maxTextLength { get { return _maxtext; } set { _maxtext = value; if(_maxtext > text.Length) text = text.Substring(0,_maxtext); } }

        int scrollLeft;

        public string leftSide = "[";
        public string rightSide = "]";

        string _PlaceHolder;
        public string PlaceHolder { get { return _PlaceHolder; } set { _PlaceHolder = value; if(_Position != null) ReRender(); } }

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

        string previousString;

        public InputField(string text, int width, int maxTextLength, string placeholder)
        {
            this.text = text;
            this.width = width;
            this._maxtext = maxTextLength;
            this.PlaceHolder = placeholder;
        }

        public InputField(string text, int width, int maxTextLength, string placeholder, string leftside, string rightside)
        {
            this.text = text;
            this.width = width;
            this.maxTextLength = maxTextLength;
            this.leftSide = leftside;
            this.rightSide = rightside;
            this.PlaceHolder = placeholder;
        }
        // i quit. its been way too long. why is it when i press backspace and there is no text, it deletes the leftSide
        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            int amount = width;
            if(width+scrollLeft > text.Length)
            {
                amount = text.Length-scrollLeft;
            }
            string s;
            if(text.Length > 0) s = leftSide + text.Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide;
            else s = leftSide + PlaceHolder.Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide;
            
            UIElement.Write(s);
            UIElement.CursorPos(scrollLeft == 0 && text.Length <= width ? Position.x + text.Length + 1: Position.x + width + 1, Position.y);
            previousString = new string(' ', s.Length);
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            UIElement.CursorPos(Position.x, Position.y);
            UIElement.Write(previousString);
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() 
        {
            Console.CursorVisible = true;
        }
        public void OnClick() { }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() 
        {
            if (scrollLeft - 1 >= 0)
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
        public void OnHoverLeave() 
        {
            Console.CursorVisible = false;
        }

        public void OnTextInput(ConsoleKeyInfo character) 
        {
            if(character.Key == ConsoleKey.Backspace && text.Length-1 >= 0)
            {
                text = text.Remove(text.Length-1, 1);
                OnLeftArrow();
            }
            else
            {
                if(text.Length+1 > maxTextLength) return;
                text += character.KeyChar;
                OnRightArrow();
            }
            if(OnValueChangeEvent != null) OnValueChangeEvent();
            ReRender();
        }
    }
}