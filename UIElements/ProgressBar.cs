using System;

namespace Renderer
{
    public class ProgressBar : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        int _progress;
        public int Progress {
            get { return _progress; }
            set { _progress = value; ReRender(); }
        }
        private int previousString;

        char leftSide = '[';
        char rightSide = ']';
        char progressChar = '#';
        char noProgress = ' ';
        
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

        private int _width;
        public int Width {get {return _width;} set { _width = value; ReRender(); }}

        public ProgressBar(int progress, int width)
        {
            this._progress = progress;
            this._width = width;

            if(width <= 0 || progress > width)
                throw new ArgumentException("ProgressBar width must be greater than 0 and progress must be less than width.");
        }

        public ProgressBar(int progress, int width, char start, char end, char progressChar, char noProgress)
        {
            this._progress = progress;
            this._width = width;
            this.leftSide = start;
            this.rightSide = end;
            this.progressChar = progressChar;
            this.noProgress = noProgress;

            if(width <= 0 || progress > width)
                throw new ArgumentException("ProgressBar width must be greater than 0 and progress must be less than width.");
        }

        public void Render()
        {
            if(this.Width <= 0 || Progress > this.Width)
                throw new ArgumentException("ProgressBar width must be greater than 0 and progress must be less than width.");
            previousString = Width + 2;
            Console.Write(leftSide);
            Console.Write(new string(progressChar, this.Progress));
            Console.Write(new string(noProgress, this.Width - this.Progress));
            Console.Write(rightSide);
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
    }
}