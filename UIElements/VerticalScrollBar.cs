using System;

namespace Renderer
{
    public class VerticalScrollBar : IInteractive
    {
        public Vector2 Position { get; set; }
        int _Progress;
        public int Progress { get { return _Progress; } set { _Progress = value; ReRender(); } }
        public int Height;

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
        
        char sliderChar = '|';
        char ends = '#';
        char noProgress = ' ';

        int previousString;

        public VerticalScrollBar(int progress, int height)
        {
            this._Progress = progress;
            this.Height = height;
        }

        public VerticalScrollBar(int progress, int height, char sliderChar, char ends, char noProgress)
        {
            this._Progress = progress;
            this.sliderChar = sliderChar;
            this.ends = ends;
            this.Height = height;
            this.noProgress = noProgress;
        }

        public void Render()
        {
            //if(Selected) Console.BackgroundColor = Window.SelectedColor;
            Console.Write(ends);
            for(int i = 0; i < Height; i++)
            {
                if (Progress == 0 && i == 1)
                    Console.Write(sliderChar);
                else if (i == Progress && i != 0)
                    Console.Write(sliderChar);
                else
                    Console.Write(noProgress);
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                previousString++;
            }
            Console.Write(ends);
            //if(Selected) Console.BackgroundColor = ConsoleColor.Black; // i shouldve put this in inputsystem isntead of putting it in every file. too bad!
        }

        public void DeRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            for(int i = 0; i < previousString; i++)
            {
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                Console.Write(" ");
            }
            previousString = 0;
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { }
        public void OnUpArrow() 
        {
            if(Progress > 0)
                Progress--;
            ReRender();
        }
        public void OnDownArrow() 
        {
            if(Progress < Height-1)
                Progress++;
            ReRender();
        }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}