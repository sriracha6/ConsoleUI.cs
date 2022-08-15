using System;

namespace Renderer
{
    // TODO: remake this to be a wrapper of a label that is constantly changed visible state
    public class Blink : IAnimatable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        private string _text;
        public string text { get {return _text;} set {_text = value; ReRender();}}
        private string previousString;

        int tickCount;

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

        public Blink(string text)
        {
            this._text = text;
            URenderer.animatableItems.Add(this);
        }

        public void Render()
        {
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

        public void Tick()
        {
            lock(new object())
            {
                Console.ResetColor();
                tickCount++;
                if(tickCount % 2 == 0) ReRender();
                else DeRender();
            }
        }
    }
}