using System;
using System.Collections.Generic;


namespace Tetris
{
    abstract class Figures
    {
        public List<Node> FigureNodes { get; set; }
        public FigureShape FigureShape { get; set; }
        protected char figureSymbol = '0';
        protected ConsoleColor iFigureColor = ConsoleColor.Cyan;
        protected ConsoleColor jFigureColor = ConsoleColor.Blue;
        protected ConsoleColor lFigureColor = ConsoleColor.Gray;
        protected ConsoleColor oFigureColor = ConsoleColor.Yellow;
        protected ConsoleColor sFigureColor = ConsoleColor.Green;
        protected ConsoleColor tFigureColor = ConsoleColor.Magenta;
        protected ConsoleColor zFigureColor = ConsoleColor.Red;
        
        public void MoveDown()
        {
            foreach (var node in FigureNodes)
            {
                node.MoveDown();
            }
        }

        public void MoveLeft()
        {
            foreach (var node in FigureNodes)
            {
                node.MoveLeft();
            }
        }

        public void MoveRight()
        {
            foreach (var node in FigureNodes)
            {
                node.MoveRight();
            }
        }
    }

    class Figure : Figures
    {
        public Figure()
        {
            FigureNodes = new List<Node>();
        }
    }

    class IFigure : Figures
    {
        public IFigure(Border border)
        {
            FigureShape = FigureShape.IFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, iFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 2, iFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 3, iFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 4, iFigureColor, figureSymbol)
            };
        }
    }

    class JFigure : Figures
    {
        public JFigure(Border border)
        {
            FigureShape = FigureShape.JFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, jFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 1, jFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 2, jFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 3, jFigureColor, figureSymbol)
            };
        }
    }

    class LFigure : Figures
    {
        public LFigure(Border border)
        {
            FigureShape = FigureShape.LFigure;
            FigureNodes = new List<Node>
            {
                
                new Node(border.Width / 2 + border.X, border.Y - 1, lFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, lFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 2, lFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 3, lFigureColor, figureSymbol)
            };
        }
    }

    class OFigure : Figures
    {
        public OFigure(Border border)
        {
            FigureShape = FigureShape.OFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, oFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 1, oFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 2, oFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 2, oFigureColor, figureSymbol)
            };
        }
    }

    class SFigure : Figures
    {
        public SFigure(Border border)
        {
            FigureShape = FigureShape.SFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X, border.Y - 1, sFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 2, sFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 2, sFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 3, sFigureColor, figureSymbol)
            };
        }
    }

    class TFigure : Figures
    {
        public TFigure(Border border)
        {
            FigureShape = FigureShape.TFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, tFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 1, tFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X + 1, border.Y - 1, tFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 2, tFigureColor, figureSymbol)
            };
        }
    }

    class ZFigure : Figures
    {
        public ZFigure(Border border)
        {
            FigureShape = FigureShape.ZFigure;
            FigureNodes = new List<Node>
            {
                new Node(border.Width / 2 + border.X - 1, border.Y - 1, zFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X - 1, border.Y - 2, zFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 2, zFigureColor, figureSymbol),
                new Node(border.Width / 2 + border.X, border.Y - 3, zFigureColor, figureSymbol)
            };
        }
    }
}
