using System;
using System.Linq;
using System.Collections.Generic;

namespace Renderer
{
    public class TextArea : IInteractive
    {
        public Vector2 Position {get; set;}
        public string text {
            get { 
                string _text = "";
                foreach(string s in lines)
                    _text += s + "\n";
                return _text;
            }
        }
        public int Width;
        public int Height;
        public int maxTextLength;

        List<string> lines = new List<string>();
        string longestLine { get { return lines.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);}}

        int scrollLeft;
        int scrollTop;

        Vector2 cursorPos = new Vector2(0,0);

        public HorizontalScrollBar hscroll;
        public VerticalScrollBar vscroll;

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

        public delegate void OnValueChange();
        public event OnValueChange OnValueChangeEvent;

        int previousStringW;
        int previousStringH;

        bool flag = false;

        public TextArea(string text, int width, int height, int maxTextLength, HorizontalScrollBar hscroller, VerticalScrollBar vscroller)
        {
            lines = text.Split('\n').ToList();
            this.Width = width;
            this.Height = height;
            this.maxTextLength = maxTextLength;
            this.hscroll = hscroller;
            this.vscroll = vscroller;
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            vscroll.Position = new Vector2(Position.x+Width, Position.y);
            hscroll.Position = new Vector2(Position.x, Position.y+Height);
            
            hscroll.ReRender();
            vscroll.ReRender();

            Console.CursorVisible = true;

            for(int i = scrollTop; i < scrollTop+Height-1; i++)
            {
                if(i > lines.Count-1)
                    break;
                Console.SetCursorPosition(Position.x, Position.y+(i-scrollTop)+1);
                int amount = Width;
                if(lines[i].Length < scrollLeft + Width)
                    amount = Width - (scrollLeft + Width - lines[i].Length);
                if(amount <= 0) continue;

                string tt = lines[i].Substring(scrollLeft, amount);
                Console.Write(tt);
            }
            Console.SetCursorPosition(Position.x+(cursorPos.x-scrollLeft), Position.y+(cursorPos.y-scrollTop)+1);

            previousStringW = Width;
            previousStringH = Height;
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            for(int i = 0; i < previousStringH; i++)
            {
                Console.Write(new string(' ', previousStringW));
                Console.SetCursorPosition(Position.x, Position.y+i+1);
            }
            previousStringW = 0;
            previousStringH = 0;
            hscroll.DeRender();
            vscroll.DeRender();
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() 
        {
            Console.CursorVisible = true;
        }
        public void OnClick() 
        {
            cursorPos.y++;
            // check if we need to scroll
            if(cursorPos.y > scrollTop+Height-1)
                scrollTop++;
            
            lines.Insert(cursorPos.y, lines[cursorPos.y-1].Substring(cursorPos.x, lines[cursorPos.y-1].Length-cursorPos.x));
            lines[cursorPos.y-1] = lines[cursorPos.y-1].Remove(cursorPos.x, lines[cursorPos.y-1].Length-cursorPos.x);
            scrollLeft = 0;
            cursorPos.x = 0;
            ReRender();
        }
        public void OnUpArrow()
        {
            if(cursorPos.y-1 > -1) cursorPos.y--;
            if (scrollTop - 1 > -1 && cursorPos.y < scrollTop)
            {
                scrollTop--;
                vscroll.Progress = Height-(int)(1f/(scrollTop + Height/2) * (lines.Count)); 
            }
            if(cursorPos.x > lines[cursorPos.y].Length) 
            {
                cursorPos.x = lines[cursorPos.y].Length;
                scrollLeft = lines[cursorPos.y].Length ;
            }
            ReRender();
        }
        public void OnDownArrow()
        {
            if(cursorPos.y+1 < lines.Count) cursorPos.y++;
            if (scrollTop + 1 < lines.Count-Height && cursorPos.y > scrollTop+Height-1)
            {
                scrollTop++;
                vscroll.Progress = Height-(int)(1f/(scrollTop + Height/2) * (lines.Count)); 
            }
            if(cursorPos.x > lines[cursorPos.y].Length)
            {
                cursorPos.x = lines[cursorPos.y].Length;
                scrollLeft = lines[cursorPos.y].Length;
            }
            ReRender();
        }
        public void OnLeftArrow() 
        {
            if(cursorPos.x-1 >= 0 && !flag) cursorPos.x--;
            if (scrollLeft-cursorPos.x > 0)
            {
                scrollLeft--;
                hscroll.Progress = Width-(int)(1f/(scrollLeft + Width/2) * (longestLine.Length)); 
            }
            flag = false;
            ReRender();
        }
        public void OnRightArrow() 
        {
            if(cursorPos.x+1 <= lines[cursorPos.y].Length && !flag) cursorPos.x++;
            if (scrollLeft < longestLine.Length - Width && cursorPos.x-scrollLeft >= Width)
            {
                scrollLeft++;
                hscroll.Progress = Width-(int)(1f/(scrollLeft + Width/2) * (longestLine.Length)); 
            }
            ReRender();
            flag = false;
        }
        public void OnHoverLeave() 
        {
            Console.CursorVisible = false;
        }

        public void OnTextInput(ConsoleKeyInfo character) 
        {
            if(character.Key == ConsoleKey.Backspace && lines[cursorPos.y].Length-1 >= 0)
            {
                if(cursorPos.x-1 >= 0 && !flag)
                cursorPos.x--;
                if(cursorPos.x < 0) {cursorPos.x = 0; return;}

                if(lines[cursorPos.y].Length == 0)
                {
                    lines.RemoveAt(cursorPos.y);
                    cursorPos.y--;
                }
                else
                    lines[cursorPos.y] = lines[cursorPos.y].Remove(cursorPos.x, 1);
                
                if(cursorPos.x < scrollLeft)
                {
                    flag = true;
                    OnLeftArrow();
                }
            }
            else
            {   
                if(lines.Sum(x=>x.Length)+1 > maxTextLength) return;
                lines[cursorPos.y] = lines[cursorPos.y].Insert(cursorPos.x, character.KeyChar.ToString());

                cursorPos.x++;
                if(cursorPos.x > scrollLeft+Width)
                {
                    flag = true;
                    OnRightArrow();
                }
            }
            if(OnValueChangeEvent != null) OnValueChangeEvent();
            ReRender();
        }
    }
}