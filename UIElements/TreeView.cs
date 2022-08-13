using System;
using System.Collections.Generic;

namespace Renderer
{
    class TreeItem {
        string Text;
        public List<TreeItem> Children;

        public TreeItem(string text, List<TreeItem> children) {
            Text = text;
            Children = children;
        }

        public TreeItem(string text) {
            Text = text;
            Children = new List<TreeItem>();
        }
         
    }
    public class TreeView : IInteractive
    {
        public Vector2 Position { get; set; }
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
        public List<TreeView> Children = new List<TreeView>();    
        public string Text;
        public bool Expanded = false;

        string Open = " ";
        string Closed = ">";
        string Tab = "  ";

        string previousString = "";

        public TreeView(string text, List<TreeView> children)
        {
            this.Text = text;
            this.Children = children;
        }

        public TreeView(string text)
        {
            this.Text = text;
            this.Children = new List<TreeView>();
        }

        public TreeView(string text, List<TreeView> children, string open, string closed, string tab)
        {
            this.Text = text;
            this.Children = children;
            this.Open = open;
            this.Closed = closed;
            this.Tab = tab;
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            string s = (Expanded ? Open : Closed)+ " " + Text;
            Console.Write(s);
            previousString = UIElement.ParsePreviousString(s);
            if(!Expanded) return;
            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].Position = new Vector2(Position.x + Tab.Length, Position.y + i + 1);
                Children[i].ReRender();
                previousString += new string(' ', Tab.Length);
                previousString += UIElement.ParsePreviousString(Children[i].Text);
            }
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            int nlcount = 1;
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
            
            if(Children.Count > 0)
                for(int i = 0; i < Children.Count; i++)
                    Children[i].DeRender();
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition((int)Position.x, (int)Position.y);
            Render();
        }

        public void OnHover() { }
        public void OnClick() { Expanded = !Expanded; ReRender(); }
        public void OnUpArrow() { }
        public void OnDownArrow() { }
        public void OnLeftArrow() { }
        public void OnRightArrow() { }
        public void OnHoverLeave() { }

        public void OnTextInput(ConsoleKeyInfo character) { }
    }
}