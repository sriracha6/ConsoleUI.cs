using System;

namespace Renderer
{
    public interface IRenderable
    {
        bool Visible { get; set; }
        Vector2 Position { get; set; }
        void Render();
        void DeRender();
        void ReRender();
    }
}