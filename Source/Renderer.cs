using System;
using System.Collections.Generic;

namespace Renderer
{
    // this class is bad
    public static class URenderer
    {
        public static List<IRenderable> itemsOrdered = new List<IRenderable>();
        public static List<IInteractive> inputItemsOrdered = new List<IInteractive>();
        public static List<IAnimatable> animatableItems = new List<IAnimatable>();
        public static List<Vector2> inputItemsPositions = new List<Vector2>();

        public static void Render(IInteractive item, Vector2 position)
        {
            if(position.x < 0 || position.y < 0)
                throw new ArgumentOutOfRangeException("Position cannot be negative");
            if(position.x > Console.WindowWidth || position.y > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("Position cannot be greater than window size");
            
            UIElement.CursorPos(position.x, position.y);
            inputItemsOrdered.Add(item);
            inputItemsPositions.Add(position);
            item.Position = position;
            item.Render();
        }

        /// <summary>
        /// Sets the tab index of an item. If index < 0, the item is removed from the tab order.
        /// </summary>
        public static void SetTabIndex(this IInteractive item, int index)
        {
            if(index < 0)
            {
                inputItemsOrdered.Remove(item);
                inputItemsPositions.Remove(item.Position);
            }
            else
            {
                inputItemsOrdered.Remove(item);
                inputItemsOrdered.Insert(index, item);
                inputItemsPositions.Remove(item.Position);
                inputItemsPositions.Insert(index, item.Position);
            }
        }

        public static void Render(IRenderable item, Vector2 position)
        {
            if(position.x < 0 || position.y < 0)
                throw new ArgumentOutOfRangeException("Position cannot be negative");
            if(position.x > Console.WindowWidth || position.y > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("Position cannot be greater than window size");
            
            UIElement.CursorPos(position.x, position.y);
            itemsOrdered.Add(item);
            item.Position = position;
            item.Render();
        }
    }
}