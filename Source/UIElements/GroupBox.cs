using System;

namespace Renderer
{
    public class GroupBox : IRenderable
    {
        string _Text; 
        Panel _Panel;

        public string Text { get{return _Text;} set {_Text = value; if(_Position!=null) ReRender(); } }
        public Panel Panel{ get{return _Panel;} set {_Panel = value; if(_Position!=null) ReRender(); } }
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
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

        public GroupBox(string text, Panel panel)
        {
            this.Text = text;
            this.Panel = panel;
        }

        public void Render()
        {
            Panel.Position = Position;
            UIElement.CursorPos(Position.x, Position.y);
            Panel.Render();
            UIElement.CursorPos(Position.x+2, Position.y);
            UIElement.Write(Text);
        }

        public void DeRender()
        {
            Panel.DeRender();
        }

        public void ReRender()
        {
            DeRender();
            UIElement.CursorPos(Position.x, Position.y);
            Render();
        }
    }
}