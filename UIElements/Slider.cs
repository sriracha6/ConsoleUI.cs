using System;

namespace Renderer
{
    public class Slider : IInteractive
    {
        public Vector2 Position {get; set;}
        public int value;
        public int maxValue;
        public int minValue;
        public int width;
        public int step;

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

        public string leftSide = "[";
        public string slider = "|";
        public string rightSide = "]";

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
            string s = leftSide + new string(' ', value/step) + slider + new string(' ',width-value) + rightSide;
            Console.Write(s);
            previousString = s.Length;
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