using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.Infrastructure
{
    public class CollisionEventArgs: EventArgs
    {
        public double CollisionPoint;

        public CollisionEventArgs(double _collisionPoint)
        {
            CollisionPoint = _collisionPoint;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
