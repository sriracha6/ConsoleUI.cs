using System;

namespace Renderer
{
    public static class Input
    {
        public static IInputable currentItem {get; private set;}
        public static int currentItemIndex {get; private set;}
        public static ConsoleKeyInfo currentKey {get; private set;}
        /// <summary>
        /// To be run in a thread. Gets the current input.
        /// </summary>
        public static void GetInput()
        {
            currentItemIndex = -1;
            for(;;)
            {
                var key = Console.ReadKey(true);
                bool pressShift = key.Modifiers == ConsoleModifiers.Shift;
                currentKey = key;
                if(key.Key == ConsoleKey.Tab && pressShift && currentItemIndex > 0)
                {
                    if(currentItem != null) currentItem.OnHoverLeave();
                    currentItemIndex--;
                    var pos = Renderer.inputItemsPositions[currentItemIndex];
                    Console.SetCursorPosition(pos.x, pos.y);
                    currentItem = Renderer.inputItemsOrdered[currentItemIndex];
                    currentItem.OnHover();
                }
                if(key.Key == ConsoleKey.Tab && !pressShift && currentItemIndex+1 < Renderer.inputItemsOrdered.Count)
                {
                    if(currentItem != null) currentItem.OnHoverLeave();
                    currentItemIndex++;
                    currentItem = Renderer.inputItemsOrdered[currentItemIndex];
                    currentItem.OnHover();
                }
                if(key.Key == ConsoleKey.Enter)
                {
                    if(currentItem != null)
                        currentItem.OnClick();
                }
                if(key.Key == ConsoleKey.UpArrow)
                {
                    if(currentItem != null)
                        currentItem.OnUpArrow();
                }
                if(key.Key == ConsoleKey.DownArrow)
                {
                    if(currentItem != null)
                        currentItem.OnDownArrow();
                }
                if(key.Key == ConsoleKey.LeftArrow)
                {
                    if(currentItem != null)
                        currentItem.OnLeftArrow();
                }
                if(key.Key == ConsoleKey.RightArrow)
                {
                    if(currentItem != null)
                        currentItem.OnRightArrow();
                }
            }
        }
    }
}