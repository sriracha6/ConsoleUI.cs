using System;
using System.Collections.Generic;

namespace Renderer
{
    // TODO: word wrap
    public class BigTextBox : IInteractive
    {
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        private string _text;
        public string text { get {return _text;} set {_text = value; SetOptions(); ReRender();}}

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

        public int Width;
        public int Height;

        ListView listBox;

        public BigTextBox(string text, int width, int height)
        {
            this._text = text;
            this.Width = width;
            this.Height = height;
            this.listBox = new ListView(GetOptions(), width, height, true);
        }

        List<string> GetOptions()
        {
            List<string> options = new List<string>();
            int i=0;
            while(true)
            {
                int s = (i*Width); if(s<0)s=0;
                int c = Width;
                if(s + c > text.Length) c = text.Length - s;
                if(c <= 0) break;

                string o = text.Substring(s,c);
                o = o.TrimStart(' ');
                options.Add(o);
                i++;
            }
            return options;
        }

        void SetOptions()
        {
            listBox.ClearItems();
            foreach(string s in GetOptions())
                listBox.AddItem(s, false);
        }

        public void Render()
        {
            if(Selected) Console.BackgroundColor = Window.SelectedColor;
            listBox.Position = Position;
            listBox.ReRender();
            if(Selected) Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DeRender()
        {
            listBox.DeRender();
        }

        public void ReRender()
        {
            DeRender();
            Console.SetCursorPosition(Position.x, Position.y);
            Render();
        }

        public void OnHover() { listBox.OnHover(); listBox.Selected = true; }
        public void OnClick() { listBox.OnClick(); }
        public void OnUpArrow() { listBox.OnUpArrow(); }
        public void OnDownArrow() { listBox.OnDownArrow(); }
        public void OnLeftArrow() { listBox.OnLeftArrow(); }
        public void OnRightArrow() { listBox.OnRightArrow(); }
        public void OnHoverLeave() { listBox.OnHoverLeave(); listBox.Selected = false; }

        public void OnTextInput(ConsoleKeyInfo character) { listBox.OnTextInput(character); }
    }
}