using System;
using System.Collections.Generic;

namespace Renderer
{ 
    public class Panel : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); positionDelta = new Vector2(_Position.x-value.x,_Position.y-value.y); _Position = value; Reposition(); } }
        Vector2 positionDelta;
        int _width;
        int _height;

        void Reposition()
        {
            if(Children != null && Children.Count > 0)
            foreach(var child in Children)
            {
                child.Position = new Vector2(child.Position.x+positionDelta.x, child.Position.y+positionDelta.y);
            }
        }

        public List<IRenderable> Children = new List<IRenderable>();

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
        string _fillChar;
        public string fillChar { get { return _fillChar; } set { _fillChar = value; if(_Position != null) ReRender(); } }
        string previousString;

        public Panel(int width, int height, BorderType borderType, string fillChar)
        {
            this.width = width;
            this.height = height;
            this.borderType = borderType;
            this.fillChar = fillChar;
            this.border = new Border(borderType);
        }

        public Panel(int width, int height, BorderType borderType, string fillChar, List<IRenderable> children)
        {
            this.width = width;
            this.height = height;
            this.borderType = borderType;
            this.fillChar = fillChar;
            this.Children = children;
            this.border = new Border(borderType);
        }

        public void DeRender()
        {
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            int nlcount = 0;
            for(int i = 0; i < previousString.Length; i++)
            {
                if(previousString[i] == ' ')
                    UIElement.Write(' ');
                else
                {
                    nlcount++;
                    UIElement.CursorPos(Position.x, Position.y+nlcount);
                }
            }
            foreach(IRenderable r in Children)
            {
                r.Visible = Visible;
                if(r.Visible) r.DeRender();
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
                        UIElement.Write(border.topLeftChar);
                    else if(i==width-1 && j==height-1)
                        UIElement.Write(border.bottomRightChar);
                    else if(i==0 && j==height-1)
                        UIElement.Write(border.topRightChar);
                    else if(i==width-1 && j==0)
                        UIElement.Write(border.bottomLeftChar);
                    else
                        if(i==0 || i==width-1)
                            UIElement.Write(border.horizontalChar);
                        else if(j==0 || j==height-1)
                            UIElement.Write(border.verticalChar);
                        else
                            UIElement.Write(fillChar);
                    
                    previousString += " ";
                }
                UIElement.CursorPos(Position.x, Position.y+i+1);
                previousString += "\n";
            }
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos(Position.x, Position.y);
            Render();
            foreach(IRenderable child in Children)
                child.ReRender();
        }
    }
}