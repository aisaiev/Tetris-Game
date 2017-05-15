using System;
using System.Collections.Generic;

namespace Tetris
{
    class Drawer
    {
        public void DrawBorder(Border border)
        {
            Console.ForegroundColor = border.Color;
            for (int i = 0; i < border.Height; i++)
            {
                for (int j = 0; j < border.Width; j++)
                {
                    if (i == 0 || i == border.Height - 1)
                    {
                        Console.Write(border.BorderSymbol);
                    }
                    else if (j == 0 || j == border.Width - 1)
                    {
                        Console.Write(border.BorderSymbol);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.MoveBufferArea(0, 0, border.Width, border.Height, border.X, border.Y);
        }

        public void DrawBorderForNextFigureAndScore(Border border)
        {
            Console.ForegroundColor = border.Color;
            for (int i = 0; i < border.Height; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i==0 || i == border.Height - 1)
                    {
                        Console.Write(border.BorderSymbol);
                    }
                    else if (i == 5)
                    {
                        Console.Write(border.BorderSymbol);
                    }
                    else if (j == 8 - 1)
                    {
                        Console.Write(border.BorderSymbol);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.MoveBufferArea(0, 0, 8, border.Height, border.Width + border.X, border.Y);
        }

        public void DrawFigure(IEnumerable<Node> nodes, Border border)
        {
            foreach (var node in nodes)
            {
                if (node.Y > border.Y)
                {
                    Console.SetCursorPosition(node.X, node.Y);
                    Console.ForegroundColor = node.Color;
                    Console.Write(node.NodeSymbol);
                }
            }
        }

        public void DrawNextFigure(IEnumerable<Node> nodes, Border border)
        {
            foreach (var node in nodes)
            {
                //Console.SetCursorPosition(node.X + 11, node.Y + 12);
                Console.SetCursorPosition(node.X + border.Width / 2 + 3, node.Y + border.Height / 2 + 4);
                Console.ForegroundColor = node.Color;
                Console.Write(node.NodeSymbol);
            }
        }

        public void DrawScore(Border border, int score)
        {
            Console.SetCursorPosition(border.Width + border.X + 1, border.Y + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SCORE");
            Console.SetCursorPosition(border.Width + border.X + 2, border.Y + 3);
            Console.WriteLine(score);
        }

        public void DrawGameOver(Border border)
        {
            Console.SetCursorPosition(border.X + 4, border.Height / 2 + border.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("GAME OVER");
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void ClearNextFigure(IEnumerable<Node> nodes, Border border)
        {
            foreach (var node in nodes)
            {
                Console.SetCursorPosition(node.X + border.Width / 2 + 3, node.Y + border.Height / 2 + 4);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(' ');
            }
        }
    }
}
