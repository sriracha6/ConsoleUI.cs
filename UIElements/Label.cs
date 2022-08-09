using System;

namespace Renderer
{
    public class Label : IRenderable
    {
        public Vector2 Position { get; set; }
        private string _text;
        public string text { get {return _text;} set {_text = value; ReRender();}}
        private string previousString;

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

        public Label(string text)
        {
            this._text = text;
        }

        public void Render()
        {
            previousString = UIElement.ParsePreviousString(text);
            Console.Write(text);
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}