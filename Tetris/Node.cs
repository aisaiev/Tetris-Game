using System;

namespace Tetris
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; private set; }
        public char NodeSymbol { get; private set; }

        public Node(int x, int y, ConsoleColor color, char nodeSymbol)
        {
            X = x;
            Y = y;
            Color = color;
            NodeSymbol = nodeSymbol;
        }

        public void MoveDown()
        {
            Y++;
        }

        public void MoveLeft()
        {
            X--;
        }

        public void MoveRight()
        {
            X++;
        }
    }
}
