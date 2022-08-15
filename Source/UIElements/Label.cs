using System;

namespace Renderer
{
    public class Label : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        string _text;
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
            UIElement.CursorPos(Position.x, Position.y);
            previousString = UIElement.ParsePreviousString(text);
            int nlcount = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] != '\n')
                    UIElement.Write(text[i]);
                else
                {
                    nlcount++;
                    UIElement.CursorPos(Position.x, Position.y+nlcount);
                }
            }
        }

        public void DeRender()
        {
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Console.ResetColor();
            int nlcount = 0;
            for(int i = 0; i < previousString.Length; i++)
            {
                if(previousString[i] != '\n') 
                    UIElement.Write(" ");
                else
                {
                    nlcount++;
                    UIElement.CursorPos(Position.x, Position.y+nlcount);
                }
            }
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos(Position.x, Position.y);
            Render();
        }
    }
}