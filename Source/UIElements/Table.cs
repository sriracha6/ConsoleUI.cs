using System;
using System.Collections.Generic;
using System.Linq;

namespace Renderer
{
    public class Table : IRenderable
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        public List<string> Cols;
        public List<int> ColWidths; // cant use tuples because my MACHINE IS SO OLD AND I HAVE c# 5!!! $!%*!*@
        public List<List<string>> Body;
        public BorderType Border;

        private int previousStringX;
        private int previousStringY;

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

        public Table(List<string> top, List<int> topWidths, List<List<string>> body, BorderType border)
        {
            this.Cols = top;
            this.ColWidths = topWidths;
            this.Body = body;
            this.Border = border;
            for(int i = 0; i < body.Count; i++)
                if(body[i].Count != top.Count)
                    throw new ArgumentOutOfRangeException("Table has uneven amount of columns: Row "+i.ToString());
            foreach(List<string> s in body)
                foreach(string s2 in s)
                    if(s2.Length > topWidths[s.IndexOf(s2)])
                        throw new ArgumentOutOfRangeException("Table has a column that is too long: Row "+body.IndexOf(s).ToString()+", Column "+s.IndexOf(s2).ToString());
        }

        public void Render()
        {
            int totalWidth = 0;
            totalWidth = ColWidths.Sum();
            int i = 0;
            foreach (string col in Cols)
            {
                UIElement.Write(col + new string(' ', ColWidths[i]-col.Length <= 0 ? 0 : ColWidths[i] - col.Length));
                i++;
            }
            UIElement.CursorPos(Position.x, Position.y+1);
            UIElement.Write(new string(new Border(Border).horizontalChar, totalWidth));
            UIElement.CursorPos(Position.x, Position.y+2);
            int j = 0;
            foreach(List<string> s in Body)
            {
                int k = 0;
                foreach(string t in s)
                {
                    UIElement.Write(t);// + new string(' ', ColWidths[j] - t.Length));
                    UIElement.CursorPos(Position.x + ColWidths.Take(k+1).Sum(), Position.y+2+j);
                    k++;
                }
                j++;
                UIElement.CursorPos(Position.x, Position.y+2+j);
            }
            previousStringX = totalWidth;
            previousStringY = Body.Count+2; // hehe body count
        }

        public void DeRender()
        {
            UIElement.CursorPos((int)Position.x, (int)Position.y);
            Console.ResetColor();
            for(int i = 0; i < previousStringY; i++)
            {
                UIElement.Write(new string(' ', previousStringX));
                UIElement.CursorPos(Position.x, Position.y+i);
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