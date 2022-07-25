using System;

namespace Renderer
{
    public interface IRenderable
    {
        Vector2 Position { get; set; }
        void Render();
        void ReRender();
    }
}