using System;

namespace Renderer
{
    public class Slider : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        int _value;
        public int value { get { return _value; } set { _value = value; if(_Position != null) ReRender(); } }
        int _maxValue;
        public int maxValue { get { return _maxValue; } set { _maxValue = value; if(_maxValue > value) value = _maxValue; if(_Position != null) ReRender(); } }  
        int _minValue;
        public int minValue { get { return _minValue; } set { _minValue = value; if(_minValue < value) value = _minValue; if(_Position != null) ReRender(); } }
        int _width;
        public int width { get { return _width; } set { _width = value; if(_Position != null) ReRender(); } }
        public int step;

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

        string leftSide = "[";
        string slider = "|";
        string rightSide = "]";

        int previousString = 0;

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        public Slider(int value, int maxValue, int minValue, int width, int step)
        {
            this.value = value;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.width = width;
            this.step = step;
        }

        public Slider(int value, int maxValue, int minValue, int width, int step, string leftSide, string slider, string rightSide)
        {
            this.value = value;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.leftSide = leftSide;
            this.slider = slider;
            this.rightSide = rightSide;
            this.width = width;
            this.step = step;
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            string s = leftSide + new string(' ', value/step) + slider + new string(' ',width-value) + rightSide;
            Console.Write(s);
            previousString = s.Length;
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Console.Write(new string(' ', previousString));
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
            if(value - step >= minValue)
            {
                value -= step;
                if(OnValueChangeEvent != null) OnValueChangeEvent();
                ReRender();
            }
        }
        public void OnRightArrow() 
        {
            if(value+step <= maxValue)
            {
                value += step;
                if(OnValueChangeEvent != null) OnValueChangeEvent();
                ReRender();
            }
        }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}