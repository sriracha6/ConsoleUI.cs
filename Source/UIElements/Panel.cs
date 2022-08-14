using System;

namespace Renderer
{ 
    public class Panel : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        int _width;
        int _height;

        public int width { get { return _width; } set { _width = value; if(_Position != null) ReRender(); } }
        public int height { get { return _height; } set { _height = value; if(_Position != null) ReRender(); } }

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

        Border border;
        public BorderType borderType { get { return border.borderType; } set { border = new Border(value); if(_Position != null) ReRender(); } }
        char _fillChar;
        public char fillChar { get { return _fillChar; } set { _fillChar = value; if(_Position != null) ReRender(); } }
        string previousString;

        public Panel(int width, int height, BorderType borderType, char fillChar)
        {
            this.width = width;
            this.height = height;
            this.borderType = borderType;
            this.fillChar = fillChar;
            this.border = new Border(borderType);
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            int nlcount = 0;
            for(int i = 0; i < previousString.Length; i++)
            {
                if(previousString[i] == ' ')
                    Console.Write(' ');
                else
                {
                    nlcount++;
                    Console.SetCursorPosition(Position.x, Position.y+nlcount);
                }
            }
            previousString = "";
        }

        public void Render()
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    if(i==0 && j==0)
                        Console.Write(border.topLeftChar);
                    else if(i==width-1 && j==height-1)
                        Console.Write(border.bottomRightChar);
                    else if(i==0 && j==height-1)
                        Console.Write(border.topRightChar);
                    else if(i==width-1 && j==0)
                        Console.Write(border.bottomLeftChar);
                    else
                        if(i==0 || i==width-1)
                            Console.Write(border.horizontalChar);
                        else if(j==0 || j==height-1)
                            Console.Write(border.verticalChar);
                        else
                            Console.Write(fillChar);
                    
                    previousString += " ";
                }
                Console.SetCursorPosition(Position.x, Position.y+i+1);
                previousString += "\n";
            }
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }
    }
}