using System;

namespace Tetris
{
    class Border
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public char BorderSymbol { get; private set; }
        public ConsoleColor Color { get; private set; }


        public Border(int borderWidth, int borderHeight, int x, int y, char borderSymbol, ConsoleColor color)
        {
            Height = borderHeight;
            Width = borderWidth;
            X = x;
            Y = y;
            BorderSymbol = borderSymbol;
            Color = color;
        }
    }
}
