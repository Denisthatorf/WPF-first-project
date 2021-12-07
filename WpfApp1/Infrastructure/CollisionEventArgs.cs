using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfApp1.FiguresOnCanvas;

namespace WpfApp1.Infrastructure
{
    public class CollisionEventArgs: EventArgs
    {
        public Point CollisionPoint { get; }
        public Figure figure { get; }
        public CollisionEventArgs(Figure _figure, Point _collisionPoint)
        {
            CollisionPoint = _collisionPoint;
            figure = _figure;
        }
        public override string ToString()
        {
            return figure.GetType().ToString();
        }
    }
}
