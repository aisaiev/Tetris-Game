using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tetris
{
    class Game
    {
        private Drawer drawer;
        private Figures figure;
        private Figures currentFigure;
        private Figures nextFigure;
        private Border border;
        private KeyboardHendler kbh;
        private Random rnd;
        private List<Node> fallenNodes;
        private List<Node> burnNodes;
        private int speed;
        private int increasedSpeed = 70;
        private int score = 0;
        private Direction direction;
        private FigurePosition currentPosition;
        private FigurePosition nextPosition;

        public void StartGame(Border border)
        {
            Console.CursorVisible = false;
            this.border = border;
            currentFigure = new Figure();
            nextFigure = new Figure();
            drawer = new Drawer();
            kbh = new KeyboardHendler();
            rnd = new Random();
            fallenNodes = new List<Node>();
            drawer.DrawBorderForNextFigureAndScore(border);
            kbh.KeyRight += MoveRight;
            kbh.KeyLeft += MoveLeft;
            kbh.KeyUp += Rotate;
            kbh.KeyDown += IncreaseSpeed;
           
            GenerateFigure();
            do
            {
                speed = 400;
                currentPosition = FigurePosition.position1;
                currentFigure.FigureNodes.Clear();
                nextFigure.FigureNodes.Clear();
                currentFigure.FigureNodes.AddRange(figure.FigureNodes);
                currentFigure.FigureShape = figure.FigureShape;
                GenerateFigure();
                nextFigure.FigureNodes.AddRange(figure.FigureNodes);

                do
                {
                    direction = Direction.Down;
                    Console.SetCursorPosition(0, 0);
                    drawer.DrawBorder(border);
                    drawer.DrawFigure(currentFigure.FigureNodes, border);
                    drawer.DrawFigure(fallenNodes, border);
                    drawer.DrawNextFigure(nextFigure.FigureNodes, border);
                    drawer.DrawScore(border, score);
                    Thread.Sleep(speed);
                    kbh.QueryKbH();
                    if (direction == Direction.Down)
                        MoveDown();

                } while (IsFigureOnTheBottom(currentFigure.FigureNodes) == false && IsFigureOnTheFallenFigures(currentFigure.FigureNodes, fallenNodes) == false);

                drawer.ClearNextFigure(nextFigure.FigureNodes, border);
                fallenNodes.AddRange(currentFigure.FigureNodes);

                for (int i = 0; i < border.Height; i++)
                {
                    if (fallenNodes.Where(x => x.Y == i).Count() == border.Width - 2)
                    {
                        fallenNodes.RemoveAll(x => x.Y == i);
                        burnNodes = fallenNodes.Where(x => x.Y < i).ToList();
                        foreach (var node in burnNodes)
                            node.MoveDown();
                        burnNodes.Clear();
                        score += 100;
                    }
                }

            } while (IsFallenFigureOnTheTop(fallenNodes) == false);
            drawer.DrawGameOver(border);
        }

        public void GenerateFigure()
        {
            switch (rnd.Next(1, 8))
            {
                case 1:
                    figure = new IFigure(border);
                    break;
                case 2:
                    figure = new JFigure(border);
                    break;
                case 3:
                    figure = new LFigure(border);
                    break;
                case 4:
                    figure = new OFigure(border);
                    break;
                case 5:
                    figure = new SFigure(border);
                    break;
                case 6:
                    figure = new TFigure(border);
                    break;
                case 7:
                    figure = new ZFigure(border);
                    break;
            }
        }

        public bool IsFigureOnTheBottom(List<Node> figureNodes)
        {
            foreach (var node in figureNodes)
                if (node.Y == border.Height + border.Y - 2)
                    return true;
            return false;
        }

        public bool IsFallenFigureOnTheTop(List<Node> fallenNodes)
        {
            foreach (var node in fallenNodes)
                if (node.Y == border.Y)
                    return true;
            return false;
        }

        public bool IsFigureOnTheLeftRightBorderFallenFigures(List<Node> figureNodes, List<Node> fallenNodes)
        {
            if (direction == Direction.Left)
            {
                foreach (var node in figureNodes)
                {
                    if (node.X - 1 == border.X)
                    {
                        return true;
                    }
                    foreach (var fnode in fallenNodes)
                    {
                        if (node.X - 1 == fnode.X && node.Y == fnode.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (direction == Direction.Right)
            {
                foreach (var node in figureNodes)
                {
                    if (node.X + 1 == border.Width + border.X - 1)
                    {
                        return true;
                    }
                    foreach (var fnode in fallenNodes)
                    {
                        if (node.X + 1 == fnode.X && node.Y == fnode.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsFigureOnTheFallenFigures(List<Node> figureNodes, List<Node> fallenNodes)
        {
            foreach (var node in figureNodes)
            {
                foreach (var fnode in fallenNodes)
                    if (node.X == fnode.X && node.Y + 1 == fnode.Y)
                        return true;
            }
            return false;
        }

        public bool IsRotateAvailable(List<Node> figureNodes, List<Node> fallenNodes, FigurePosition nextPosition)
        {
            foreach (var node in figureNodes)
            {
                if (node.X - 1 == border.X || node.X + 1 == border.Width + border.X - 1)
                {
                    return false;
                }
                foreach (var fnode in fallenNodes)
                {
                    if ((node.X+1 == fnode.X && node.Y+1 == fnode.Y) || (node.X-1 == fnode.X && node.Y-1 == fnode.Y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            direction = Direction.Left;
            if (IsFigureOnTheLeftRightBorderFallenFigures(currentFigure.FigureNodes, fallenNodes) == false)
                currentFigure.MoveLeft();
        }

        public void MoveRight(object sender, EventArgs e)
        {
            direction = Direction.Right;
            if (IsFigureOnTheLeftRightBorderFallenFigures(currentFigure.FigureNodes, fallenNodes) == false)
                currentFigure.MoveRight();
        }

        public void MoveDown()
        {
            if (IsFigureOnTheBottom(currentFigure.FigureNodes) == false)
                currentFigure.MoveDown();
        }

        public void IncreaseSpeed(object sender, EventArgs e)
        {
            speed = increasedSpeed;
        }

        public void Rotate(object sender, EventArgs e)
        {
            if (IsRotateAvailable(currentFigure.FigureNodes, fallenNodes, nextPosition) == true)
            {
                switch (currentFigure.FigureShape)
                {
                    case FigureShape.IFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].X -= 1; currentFigure.FigureNodes[0].Y += 1;
                                        currentFigure.FigureNodes[2].X += 1; currentFigure.FigureNodes[2].Y -= 1;
                                        currentFigure.FigureNodes[3].X += 2; currentFigure.FigureNodes[3].Y -= 2;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].X -= 1; currentFigure.FigureNodes[0].Y -= 1;
                                        currentFigure.FigureNodes[2].X += 1; currentFigure.FigureNodes[2].Y += 1;
                                        currentFigure.FigureNodes[3].X += 2; currentFigure.FigureNodes[3].Y += 2;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].X += 1; currentFigure.FigureNodes[0].Y -= 1;
                                        currentFigure.FigureNodes[2].X -= 1; currentFigure.FigureNodes[2].Y += 1;
                                        currentFigure.FigureNodes[3].X -= 2; currentFigure.FigureNodes[3].Y += 2;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].X += 1; currentFigure.FigureNodes[0].Y += 1;
                                        currentFigure.FigureNodes[2].X -= 1; currentFigure.FigureNodes[2].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 2; currentFigure.FigureNodes[3].Y -= 2;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                    case FigureShape.JFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].X -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].Y -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].X += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].Y += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                    case FigureShape.LFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].Y += 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].X -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].Y -= 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].X += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                    case FigureShape.SFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].Y += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].X -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].Y -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].X += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                    case FigureShape.TFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].X -= 1; currentFigure.FigureNodes[0].Y -= 1;
                                        currentFigure.FigureNodes[2].X += 1; currentFigure.FigureNodes[2].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].X += 1; currentFigure.FigureNodes[0].Y -= 1;
                                        currentFigure.FigureNodes[2].X -= 1; currentFigure.FigureNodes[2].Y += 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].X += 1; currentFigure.FigureNodes[0].Y += 1;
                                        currentFigure.FigureNodes[2].X -= 1; currentFigure.FigureNodes[2].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].X -= 1; currentFigure.FigureNodes[0].Y += 1;
                                        currentFigure.FigureNodes[2].X += 1; currentFigure.FigureNodes[2].Y -= 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                    case FigureShape.ZFigure:
                        {
                            NextPosition(currentPosition);
                            switch (nextPosition)
                            {
                                case FigurePosition.position1:
                                    {
                                        currentFigure.FigureNodes[0].X -= 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position1;
                                    }
                                    break;
                                case FigurePosition.position2:
                                    {
                                        currentFigure.FigureNodes[0].Y -= 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y -= 1;
                                        currentFigure.FigureNodes[3].X += 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position2;
                                    }
                                    break;
                                case FigurePosition.position3:
                                    {
                                        currentFigure.FigureNodes[0].X += 2;
                                        currentFigure.FigureNodes[1].X += 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y += 1;
                                        currentPosition = FigurePosition.position3;
                                    }
                                    break;
                                case FigurePosition.position4:
                                    {
                                        currentFigure.FigureNodes[0].Y += 2;
                                        currentFigure.FigureNodes[1].X -= 1; currentFigure.FigureNodes[1].Y += 1;
                                        currentFigure.FigureNodes[3].X -= 1; currentFigure.FigureNodes[3].Y -= 1;
                                        currentPosition = FigurePosition.position4;
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public void NextPosition(FigurePosition position)
        {
            if (currentPosition == FigurePosition.position1)
            {
                nextPosition = FigurePosition.position2;
            }
            else if (currentPosition == FigurePosition.position2)
            {
                nextPosition = FigurePosition.position3;
            }
            else if (currentPosition == FigurePosition.position3)
            {
                nextPosition = FigurePosition.position4;
            }
            else if (currentPosition == FigurePosition.position4)
            {
                nextPosition = FigurePosition.position1;
            }
        }

    }
}
