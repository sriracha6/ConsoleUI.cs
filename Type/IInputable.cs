using System;

namespace Renderer
{
    public interface IInputable
    {
        void OnHover();
        void OnClick();
        void OnUpArrow();
        void OnDownArrow();
        void OnLeftArrow();
        void OnRightArrow();
        void OnHoverLeave();

        void OnTextInput(ConsoleKey character);
    }
}