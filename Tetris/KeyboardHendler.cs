using System;

namespace Tetris
{
    class KeyboardHendler
    {
        public event EventHandler KeyLeft;
        public event EventHandler KeyRight;
        public event EventHandler KeyDown;
        public event EventHandler KeyUp;

        protected virtual void OnKeyLeft()
        {
            KeyLeft?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnKeyRight()
        {
            KeyRight?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnKeyDown()
        {
            KeyDown?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnKeyUp()
        {
            KeyUp?.Invoke(this, EventArgs.Empty);
        }

        public void QueryKbH()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            OnKeyLeft();
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            OnKeyRight();
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            OnKeyDown();
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            OnKeyUp();
                            break;
                        }
                }
            }

        }
    }
}
