using System;

namespace Renderer
{
    public static class Input
    {
        public static IInteractive currentItem {get; private set;}
        public static int currentItemIndex {get; private set;}
        public static ConsoleKeyInfo currentKey {get; private set;}
        /// <summary>
        /// To be run in a thread. Gets the current input.
        /// </summary>
        internal static void GetInput()
        {
            currentItemIndex = -1;
            for(;;)
            {
                var key = Console.ReadKey(true);
                bool pressShift = key.Modifiers == ConsoleModifiers.Shift;
                currentKey = key;
                if(key.Key == ConsoleKey.Tab && pressShift)
                {
                    Console.CursorVisible = false;
                    if(currentItem != null) 
                    { 
                        currentItem.OnHoverLeave();
                        currentItem.Selected = false;
                        currentItem.ReRender(); 
                    } 
                    if(currentItemIndex > 0)
                        currentItemIndex--;
                    else
                        currentItemIndex = Renderer.inputItemsOrdered.Count - 1;
                    Console.ResetColor();
                    var pos = Renderer.inputItemsPositions[currentItemIndex];
                    Console.SetCursorPosition(pos.x, pos.y);
                    currentItem = Renderer.inputItemsOrdered[currentItemIndex];
                    currentItem.Selected = true;
                    currentItem.OnHover();
                    currentItem.ReRender();
                }
                else if(key.Key == ConsoleKey.Tab && !pressShift && currentItemIndex+1 < Renderer.inputItemsOrdered.Count)
                {
                    Console.CursorVisible = false;
                    if(currentItem != null) 
                    { 
                        currentItem.OnHoverLeave();
                        currentItem.Selected = false;
                        currentItem.ReRender(); 
                    } 
                    Console.ResetColor();
                    currentItemIndex++;
                    currentItem = Renderer.inputItemsOrdered[currentItemIndex];
                    currentItem.Selected = true;
                    currentItem.OnHover();
                    currentItem.ReRender();
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnClick();
                }
                else if(key.Key == ConsoleKey.UpArrow)
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnUpArrow();
                }
                else if(key.Key == ConsoleKey.DownArrow)
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnDownArrow();
                }
                else if(key.Key == ConsoleKey.LeftArrow)
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnLeftArrow();
                }
                else if(key.Key == ConsoleKey.RightArrow)
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnRightArrow();
                }
                else
                {
                    if(currentItem != null && currentItem.Visible)
                        currentItem.OnTextInput(key);
                }
            }
        }
    }
}