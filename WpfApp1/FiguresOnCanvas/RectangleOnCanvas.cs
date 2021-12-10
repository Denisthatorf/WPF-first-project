using System;
using System.Numerics;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class RectangleOnCanvas : Figure
    {
        public RectangleOnCanvas() : base()
        {
        }

        public override bool IsCollide(Figure figure)
        {
            if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
            if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;
            if (!(figure is RectangleOnCanvas) || figure == this || figure == null) return false;

            return true;
        }

        public override Point PointOfCollision(Figure figure)
        {
            throw new NotImplementedException();
        }
    }
}
