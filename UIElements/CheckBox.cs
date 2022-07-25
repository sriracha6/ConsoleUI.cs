using System;

namespace Renderer
{
    public class CheckBox : IInteractive
    {
        public Vector2 Position { get; set; }
        private bool _isChecked;
        public bool IsChecked {get {return _isChecked;} set{_isChecked = value; ReRender();}}
        private string _Text;
        public string Text {get {return _Text;} set {_Text = value; ReRender();} }
        public delegate void OnValueChangeDelegate();
        public event OnValueChangeDelegate OnValueChange;
        
        string leftSide = "[";
        string checkedC = "X";
        string rightSide = "]";

        private string previousString;

        public CheckBox(string text, bool isChecked)
        {
            this._Text = text.Replace("\n","");
            this._isChecked = isChecked;
        }

        public CheckBox(string text, bool isChecked, string leftSide, string checkedC, string rightSide)
        {
            this._Text = text.Replace("\n","");
            this._isChecked = isChecked;
            this.leftSide = leftSide;
            this.checkedC = checkedC;
            this.rightSide = rightSide;
        }

        public void OnHover() { }
        public void OnClick() 
        {
             IsChecked = !IsChecked;
            if(OnValueChange!=null) OnValueChange(); 
            ReRender();
        }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void Render()
        {
            string s = leftSide + (IsChecked ? checkedC : " ") + rightSide + " " + Text;
            previousString = UIElement.ParsePreviousString(s);
            Console.Write(s);
        }
        public void ReRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Console.Write(previousString);
            Render();
        }
    }
}