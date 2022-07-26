using System;
using System.Collections.Generic;

namespace Renderer
{
    public static class Renderer
    {
        public static List<IRenderable> itemsOrdered = new List<IRenderable>();
        public static List<IInteractive> inputItemsOrdered = new List<IInteractive>();
        public static List<Vector2> inputItemsPositions = new List<Vector2>();

        public static void Render(IInteractive item, Vector2 position)
        {
            if(position.x < 0 || position.y < 0)
                throw new ArgumentOutOfRangeException("Position cannot be negative");
            if(position.x > Console.WindowWidth || position.y > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("Position cannot be greater than window size");
            
            Console.SetCursorPosition(position.x, position.y);
            inputItemsOrdered.Add(item);
            inputItemsPositions.Add(position);
            item.Position = position;
            item.Render();
        }

        public static void Render(IRenderable item, Vector2 position)
        {
            if(position.x < 0 || position.y < 0)
                throw new ArgumentOutOfRangeException("Position cannot be negative");
            if(position.x > Console.WindowWidth || position.y > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("Position cannot be greater than window size");
            
            Console.SetCursorPosition(position.x, position.y);
            itemsOrdered.Add(item);
            item.Position = position;
            item.Render();
        }
    }
}