using System;
using System.Threading;

namespace Renderer
{
    public class Program
    {
        public static void Main()
        {
            int clicks = 0;
            Console.Clear();
            Thread inputThread = new Thread(Input.GetInput);
            inputThread.Start();
            Label s = new Label("Hello, World!");
            ProgressBar p = new ProgressBar(3, 7);
            CheckBox cb = new CheckBox("Check me!", false);
            Button button = new Button("Click me!", delegate {clicks++;s.text = "You clicked the button "+clicks+" times!";});
            Panel panel = new Panel(20,20,BorderType.Single, ' ');

            Renderer.Render(panel, new Vector2(0,0));
            Renderer.Render(s, new Vector2(21, 0));
            Renderer.Render(p, new Vector2(2, 1));
            Renderer.Render(cb, new Vector2(4, 2));
            Renderer.Render(button, new Vector2(6, 3));
        }
    }
}