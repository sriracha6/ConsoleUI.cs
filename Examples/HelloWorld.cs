using System;
using Renderer;

class Example1
{
    static void Prog()
    {
        Console.Clear();
        Window.Init(120,30, ConsoleColor.DarkBlue);
        Label s = new Label("Hello, World!");
        Renderer.Render(s, new Vector2(0,0));
    }
}