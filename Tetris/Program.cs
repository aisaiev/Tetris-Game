using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Border border = new Border(16, 16, 0, 0, '#', ConsoleColor.White);
            game.StartGame(border);
        }
    }
}
