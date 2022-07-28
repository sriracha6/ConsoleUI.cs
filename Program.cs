using System;
using System.Collections.Generic;
using System.Threading;

namespace Renderer
{
    public class Program
    {
        public static void Main()
        {
            int clicks = 0;
            Console.Clear();

            Window.Init(120,30);

            Label s = new Label("Hello, World!");
            ProgressBar p = new ProgressBar(3, 7);
            CheckBox cb = new CheckBox("Check me!", false);
            Button button = new Button("Click me!", delegate {clicks++;s.text = "You clicked the button "+clicks+" times!";});
            Panel panel = new Panel(20,20,BorderType.Single, ' ');
            RadioGroup radioGroup = new RadioGroup(new List<string>() {"Pick", "one", "of", "us"});
            OptionGroup optionGroup = new OptionGroup(new List<string>() {"Or", "Select", "Us" });
            Slider slider = new Slider(0, 5, 0, 5, 1);
            InputField inputfield = new InputField("Type here", 10, 100);
            IntInputField intinputfield = new IntInputField(0, 5, 100, -5);

            VerticalScrollBar vscroller = new VerticalScrollBar(0, 10);
            ListView listView = new ListView(new List<string>() {"Hello", "World", "Amirite!", "However", "This", "Panel", "contains", "a", "lot so", "you may", "need", "to scroll"}, 10, 5);;

            Renderer.Render(panel, new Vector2(0,0));
            Renderer.Render(listView, new Vector2(30,6));
            Renderer.Render(s, new Vector2(21, 0));
            Renderer.Render(p, new Vector2(2, 1));
            Renderer.Render(cb, new Vector2(4, 2));
            Renderer.Render(button, new Vector2(6, 3));
            Renderer.Render(radioGroup, new Vector2(21, 4));
            Renderer.Render(optionGroup, new Vector2(21, 9));
            Renderer.Render(slider, new Vector2(1, 6));
            Renderer.Render(inputfield, new Vector2(15,15));
            Renderer.Render(intinputfield, new Vector2(15,16));
        }
    }
}