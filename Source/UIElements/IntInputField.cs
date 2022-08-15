using System;

namespace Renderer
{
    public class IntInputField : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        int _text;
        public int text { get { return _text; } set { _text = value; if(_Position != null) ReRender(); } }
        int _width;
        public int width { get { return _width; } set { _width = value; if(_Position != null) ReRender(); } }
        int _maxtext;
        public int maxValue { get { return _maxtext; } set { _maxtext = value; if(_maxtext > text) text = _maxtext; if(_Position != null) ReRender(); } }
        int _mintext;
        public int minValue { get { return _mintext; } set { _mintext = value; if(_mintext < text) text = _mintext; if(_Position != null) ReRender(); } }

        int scrollLeft;

        string _PlaceHolder;
        public string PlaceHolder { get { return _PlaceHolder; } set { _PlaceHolder = value; if(_Position != null) ReRender(); } }

        string leftSide = "[";
        string rightSide = "]";
        string upDown = "^v";

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

        public IntInputField(int text, int width, int maxValue, int minValue, string placeholder)
        {
            this.text = text;
            this.width = width;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.PlaceHolder = placeholder;
        }

        public IntInputField(int text, int width, int maxValue, int minValue, string placeholder, string leftside, string rightside, string upDown)
        {
            this.text = text;
            this.width = width;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.leftSide = leftside;
            this.rightSide = rightside;
            this.upDown = upDown;
            this.PlaceHolder = placeholder;
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            int amount = width;
            if(width+scrollLeft > text.ToString().Length)
            {
                amount = text.ToString().Length-scrollLeft;
            }
            string s;
            if(text.ToString().Length > 0) s = leftSide + text.ToString().Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide + upDown;
            else s = leftSide + PlaceHolder.ToString().Substring(scrollLeft, amount) + new string('.', width-amount) + rightSide + upDown;
            
            UIElement.Write(s);
            UIElement.CursorPos(scrollLeft == 0 && text.ToString().Length <= width ? Position.x + text.ToString().Length + 1 : Position.x + width + 1, Position.y);
            previousString = UIElement.ParsePreviousString(s);
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

        void Clamp() { if(text > maxValue) text = maxValue;
            if(text < minValue) text = minValue;}
        public void OnHover() 
        {
            Console.CursorVisible = true;
        }
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
        public void OnHoverLeave() 
        {
            Console.CursorVisible = false;
        }

        public void OnTextInput(ConsoleKeyInfo character) 
        {
            if(character.Key == ConsoleKey.Backspace && text >= 0)
            {
                if(text.ToString().Length == 1)
                    text = 0;
                else
                    text = int.Parse(text.ToString().Remove(text.ToString().Length-1, 1));
                OnLeftArrow();
                if(OnValueChangeEvent != null) OnValueChangeEvent();
            }
            else
            {
                int a;
                if(int.TryParse(character.KeyChar.ToString(), out a))
                {
                    text = int.Parse((text.ToString() + character.KeyChar.ToString()));
                    OnRightArrow();
                    if(OnValueChangeEvent != null) OnValueChangeEvent();
                }
                else if(character.Key == ConsoleKey.OemMinus)
                {
                    text = -text;
                    if(OnValueChangeEvent != null) OnValueChangeEvent();
                }
            }
            Clamp();
            ReRender();
        }
    }
}