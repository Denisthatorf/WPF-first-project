
using System;
using System.Numerics;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class CircleOnCanvas : Figure
    {

        public CircleOnCanvas() : base()
        {
            Radius = Width / 2;
            Mass = 3.14 * Radius;
        }

        public override bool IsCollide(Figure figure)
        {

            if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
            if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;

            if (!(figure is CircleOnCanvas) || figure == this || figure == null) return false;

            double r = this.Radius + figure.Radius;
            r *= r;
            return r + 1 > Math.Pow(this.CentreX - figure.CentreX, 2) + Math.Pow(this.CentreY - figure.CentreY, 2);
        }

        public override Point PointOfCollision(Figure figure)
        {
            return new Point( (this.CentreX + figure.CentreX)/2, (this.CentreY + figure.CentreY) / 2);
        }
    }
}
