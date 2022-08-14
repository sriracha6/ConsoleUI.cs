using System;

namespace Renderer
{
    public class AnsiString
    {
        string Text;

        public AnsiString(string text)
        {
            this.Text = text;
        }

        public static implicit operator AnsiString(string text)
        {
            return new AnsiString(text);
        }

        public static implicit operator string(AnsiString text)
        {
            return text.Text;
        }

        public static AnsiString operator +(AnsiString text, string text2)
        {
            return new AnsiString(text.Text + text2);
        }

        public static AnsiString operator +(string text, AnsiString text2)
        {
            return new AnsiString(text + text2.Text);
        }

        public static AnsiString operator +(AnsiString text, AnsiString text2)
        {
            return new AnsiString(text.Text + text2.Text);
        }

        public static AnsiString operator +(AnsiString text, object text2)
        {
            return new AnsiString(text.Text + text2);
        }
   }

   public static partial class AnsiStringExtensions
   {
        public static AnsiString Bold(this string text)
        {
            return new AnsiString("\x1b[1m" + text + "\x1b[0m");
        }

        public static AnsiString Color(this string text, System.Drawing.Color color)
        {
            return new AnsiString("\x1b[38;2;" + color.R + ";" + color.G + ";" + color.B + "m" + text + "\x1b[0m");
        }

        public static AnsiString Highlight(this string text, System.Drawing.Color color)
        {
            return new AnsiString("\x1b[48;2;" + color.R + ";" + color.G + ";" + color.B + "m" + text + "\x1b[0m");
        }

        public static AnsiString Underline(this string text)
        {
            return new AnsiString("\x1b[4m" + text + "\x1b[0m");
        }

        public static AnsiString Reverse(this string text)
        {
            return new AnsiString("\x1b[7m" + text + "\x1b[0m");
        }
   }
}