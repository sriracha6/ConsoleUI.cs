using System;

namespace Renderer
{
    public class Label : IRenderable
    {
        public Vector2 Position { get; set; }
        private string _text;
        public string text { get {return _text;} set {_text = value; ReRender();}}
        private string previousString;

        public Label(string text)
        {
            this._text = text;
        }

        public void Render()
        {
            previousString = UIElement.ParsePreviousString(text);
            Console.Write(text);
        }
        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}