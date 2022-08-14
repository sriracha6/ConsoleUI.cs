using System;

namespace Renderer
{
    public class Border
    {
        public char topLeftChar { get; private set; }
        public char topRightChar { get; private set; }
        public char bottomLeftChar { get; private set; }
        public char bottomRightChar { get; private set; }
        public char horizontalChar { get; private set; }
        public char verticalChar { get; private set; }

        public BorderType borderType { get; private set; }

        public Border(BorderType bt)
        {
            borderType = bt;
            switch(bt)
            {
                case BorderType.PlusAndLines:
                    topLeftChar = '+';
                    topRightChar = '+';
                    bottomLeftChar = '+';
                    bottomRightChar = '+';
                    horizontalChar = '-';
                    verticalChar = '|';
                    break;
                case BorderType.Solid:
                    topLeftChar = Blocks.Full;
                    topRightChar = Blocks.Full;
                    bottomLeftChar = Blocks.Full;
                    bottomRightChar = Blocks.Full;
                    horizontalChar = Blocks.Full;
                    verticalChar = Blocks.Full;
                    break;
                case BorderType.None:
                    topLeftChar = ' ';
                    topRightChar = ' ';
                    bottomLeftChar = ' ';
                    bottomRightChar = ' ';
                    horizontalChar = ' ';
                    verticalChar = ' ';
                    break;
                case BorderType.Single:
                    topLeftChar = '┌';
                    topRightChar = '┐';
                    bottomLeftChar = '└';
                    bottomRightChar = '┘';
                    horizontalChar = '─';
                    verticalChar = '│';
                    break;
                case BorderType.Double:
                    topLeftChar = '╔';
                    topRightChar = '╗';
                    bottomLeftChar = '╚';
                    bottomRightChar = '╝';
                    horizontalChar = '═';
                    verticalChar = '║';
                    break;
                case BorderType.Thick:
                    topLeftChar = '▛';
                    topRightChar = '▜';
                    bottomLeftChar = '▙';
                    bottomRightChar = '▟';
                    horizontalChar = '▄';
                    verticalChar = '█';
                    break;       
                case BorderType.PictureFrame:
                    topLeftChar = '▛';
                    topRightChar = '▜';
                    bottomLeftChar = '▙';
                    bottomRightChar = '▟';
                    horizontalChar = ' ';
                    verticalChar = ' ';
                    break;    
            }
        }
    }
}