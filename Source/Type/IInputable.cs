using System;

namespace Renderer
{
    public interface IInputable
    {
        bool Selected { get; set; }
        void OnHover();
        void OnClick();
        void OnUpArrow();
        void OnDownArrow();
        void OnLeftArrow();
        void OnRightArrow();
        void OnHoverLeave();

        void OnTextInput(ConsoleKeyInfo character);
    }
}