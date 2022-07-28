using System;

namespace Renderer
{ 
    public class Panel : IRenderable
    {
        public Vector2 Position { get; set; }

        int width;
        int height;

        bool _Visible;
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
        BorderType borderType;
        char fillChar;
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
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(previousString);
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
                Console.Write("\n");
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