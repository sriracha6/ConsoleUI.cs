using System;

namespace Renderer
{
    public class Button : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
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
        private string _Text;
        public string Text { get { return _Text; } set { _Text = value; ReRender(); } }
        private string previousString;
        public Action onClick { get; set; }

        string leftSide = "[";
        string rightSide = "]";

        public Button(string text)
        {
            this._Text = text;
        }
        public Button(string text, Action OnClick)
        {
            this._Text = text;
            this.onClick = OnClick;
        }
        public Button(string text, Action OnClick, string leftSide, string rightSide)
        {
            this._Text = text;
            this.onClick = OnClick;
            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }
        public Button(string text, string leftSide, string rightSide)
        {
            this._Text = text;
            this.leftSide = leftSide;
            this.rightSide = rightSide;
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            string s = leftSide + " " + Text + " " + rightSide;
            previousString = UIElement.ParsePreviousString(s);
            UIElement.Write(s);
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

        public void OnHover() { }
        public void OnClick() { if(onClick!=null) this.onClick(); }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }
        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}