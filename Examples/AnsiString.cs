using System;
using Renderer;

class Example2
{
    static void Prog()
    {
        Console.Clear();
        Window.Init(120,30, ConsoleColor.DarkBlue);
        Label s = new Label("Hello, World! " + "Bold ".Bold() + "Green".Color(System.Drawing.Color.Green) + "Underline".Underline());
        Button b = new Button("Click me!", "[".Color(System.Drawing.Color.Red), "]".Color(System.Drawing.Color.Red));
        URenderer.Render(s, new Vector2(0,0));
        URenderer.Render(b, new Vector2(0,1));
    }
}