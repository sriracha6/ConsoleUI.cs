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

            Window.Init(120,30, ConsoleColor.DarkBlue);

            Label s = new Label("Hello, World! " + "AnsiString ".Bold() + "Test!".Color(System.Drawing.Color.CornflowerBlue));
            ProgressBar p = new ProgressBar(3, 7);
            CheckBox cb = new CheckBox("Check me!", false);
            Button button = new Button("Click me!", delegate {clicks++;s.text = "You clicked the button "+clicks+" times!";});
            RadioGroup radioGroup = new RadioGroup(new List<string>() {"Pick", "one", "of", "us"});
            OptionGroup optionGroup = new OptionGroup(new List<string>() {"Or", "Select", "Us" });
            Slider slider = new Slider(0, 5, 0, 5, 1);
            InputField inputfield = new InputField("", 10, 100, "Type here");
            IntInputField intinputfield = new IntInputField(0, 5, 100, -5, "Num");

            GroupBox gBox = new GroupBox("Stuff", new Panel(20, 20, BorderType.Single, ' '));

            VerticalScrollBar vscroller = new VerticalScrollBar(0, 10);
            BigTextBox listView = new BigTextBox("This text is too big for this box. So, a scroller will appear and you can scroll it. This UI element is a wrapper of another one that can display single lines.", 10, 5);
            SelectListView sView = new SelectListView(new List<string>() {"This", "is", "a", "selectable", "list", "view", "It", "is", "hard", "to", "control", "imo"}, 10, 5, ConsoleColor.DarkGray, true);
            HorizontalScrollBar hscroller = new HorizontalScrollBar(0,10);

            TextArea ta = new TextArea("This. is a textarea. \nIt probably took a while for me to make. \nThis particular one has a text length limit of 1000\nHave fun typing away.\ntest\ntest\ntest\ntest\ntest\ntest\ntest\ntest\ntest\ntest\ntest", 10,10, 1000, true, new HorizontalScrollBar(0, 10), new VerticalScrollBar(0,10));
            TreeView tv = new TreeView("Test Treeview", new List<TreeView>() { new TreeView("Lol!")});

            Renderer.Render(gBox, new Vector2(0,0));
            Renderer.Render(s, new Vector2(21, 0));
            //Renderer.Render(tv, new Vector2(60, 0));
            Renderer.Render(p, new Vector2(2, 1));
            Renderer.Render(cb, new Vector2(4, 2));
            Renderer.Render(button, new Vector2(6, 3));
            Renderer.Render(radioGroup, new Vector2(21, 4));
            Renderer.Render(optionGroup, new Vector2(21, 9));
            Renderer.Render(slider, new Vector2(1, 6));
            Renderer.Render(inputfield, new Vector2(15,15));
            Renderer.Render(intinputfield, new Vector2(15,16));
            Renderer.Render(listView, new Vector2(30,6));
            Renderer.Render(sView, new Vector2(30,13));
            Renderer.Render(ta, new Vector2(50, 5));
        }
    }
}