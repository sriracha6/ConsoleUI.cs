using System;

namespace Renderer
{
    public class Marquee : IAnimatable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        private string _text;
        public string text { get {return _text;} set {_text = value + new string(' ', Width); tickCount = 0; ReRender();}}

        int tickCount;
        int revolutions;

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

        Label label;
        public int Width;

        public Marquee(string text, int width)
        {
            this._text = text + new string(' ', width);
            this.Width = width;
            label = new Label(text.Substring(0, width));
            URenderer.animatableItems.Add(this);
        }

        public void Render()
        {
            label.Position = Position;
            label.Render();
        }

        public void DeRender()
        {
            label.DeRender();
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos(Position.x, Position.y);
            Render();
        }

        public void Tick()
        {
            /*
            Hold on, can't we just do: 
			message = message.substr(1) + message.substr(0,1);
			message.substr(0,width);

            todo..
            */
            lock(new object())
            {
                Console.ResetColor();
                if(tickCount + Width >= text.Length || tickCount < 0)
                {
                    revolutions++;
                    if(revolutions % 2 == 0) tickCount = 0;
                    else tickCount = text.Length;
                    label.text = text.Substring(tickCount, tickCount + Width > text.Length ? text.Length - tickCount : Width);
                }
                else 
                    label.text = text.Substring(tickCount, Width);
                if (revolutions % 2 == 0) tickCount++;
                else tickCount--;
            }
        }
    }
}