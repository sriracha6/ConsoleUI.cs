using System;

namespace Renderer
{
    public class VerticalScrollBar : IInteractive
    {
        public Vector2 Position { get; set; }
        public int Progress;
        public int Height;
        
        char sliderChar = '|';
        char ends = '#';
        char noProgress = ' ';

        int previousString;

        public VerticalScrollBar(int progress, int height)
        {
            this.Progress = progress;
            this.Height = height;
        }

        public VerticalScrollBar(int progress, int height, char sliderChar, char ends, char noProgress)
        {
            this.Progress = progress;
            this.sliderChar = sliderChar;
            this.ends = ends;
            this.Height = height;
            this.noProgress = noProgress;
        }

        public void Render()
        {
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
        }

        public void ReRender()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            for(int i = 0; i < previousString; i++)
            {
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                Console.Write(" ");
            }
            previousString = 0;
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