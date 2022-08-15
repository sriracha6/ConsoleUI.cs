using System;
using Renderer;

// if you would like an interactive element, inherit IInteractive. If you would like it to be just rendered, inherit IRenderable. If you would like an animatable element, inherit IAnimatable
// then implement the interface
// SAMPLE:

/*
        Vector2 _Position;
        public Vector2 Position { get { return _Position; } set { if(_Position != null) DeRender(); _Position = value; } }
        string _text;
        public string text { get {return _text;} set {_text = value; ReRender();}}
        private string previousString;

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

*/

// NOTE:
//  You have to use UIElement.CursorPos(); to position cursor instead of Console.SetCursorPosition();
//  This also applies to Console.Write(); and Console.WriteLine();

// The contents of ReRender() should almost always be:
// DeRender();
// UIElement.CursorPos(Position.x, Position.y);
// Render();

// if you are making an animated element, you need to add this line to your constructor:
//            URenderer.animatableItems.Add(this);