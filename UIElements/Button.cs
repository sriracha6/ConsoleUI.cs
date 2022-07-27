using System;

namespace Renderer
{
    public class Button : IInteractive
    {
        public Vector2 Position { get; set; }
        private string _Text;
        public string Text { get { return _Text; } set { _Text = value; ReRender(); } }
        private string previousString;
        public Action onClick { get; set; }

        string leftSide = "[";
        string rightSide = "]";

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

        public void Render()
        {
            string s = leftSide + " " + Text + " " + rightSide;
            previousString = UIElement.ParsePreviousString(s);
            Console.Write(s);
        }

        public void ReRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Console.Write(previousString);
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { this.onClick(); }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }
        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}