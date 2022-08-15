using System;

namespace Renderer
{
    public class CheckBox : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        private bool _isChecked;
        public bool IsChecked {get {return _isChecked;} set{_isChecked = value; ReRender();}}
        private string _Text;
        public string Text {get {return _Text;} set {_Text = value; ReRender();} }
        public delegate void OnValueChangeDelegate();
        public event OnValueChangeDelegate OnValueChange;
        
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
        public void OnTextInput(ConsoleKeyInfo character) { }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            string s = leftSide + (IsChecked ? checkedC : " ") + rightSide + " " + Text;
            previousString = UIElement.ParsePreviousString(s);
            UIElement.Write(s);
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DeRender()
        {
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            UIElement.Write(previousString);
        }
        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Render();
        }
    }
}