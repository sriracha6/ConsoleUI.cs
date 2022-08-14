using System;

namespace Renderer
{
    public interface IAnimatable : IRenderable
    {
        void Tick();
    }
}