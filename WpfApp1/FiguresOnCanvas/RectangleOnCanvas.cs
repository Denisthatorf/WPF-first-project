using System;
using System.Numerics;
using System.Runtime.Serialization;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class RectangleOnCanvas : Figure
    {
        public RectangleOnCanvas() : base()
        {
            Radius = Width / 2 * Math.Sqrt(2);
        }

        public override bool IsCollide(Figure figure)
        {
            if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
            if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;

            return true;
        }

        public override void OnCollision(Figure figure)
        {
            if (figure is RectangleOnCanvas &&
                figure != this)
            {
                var value = figure.Velocity;

                float invLen = 1f / MathF.Sqrt(value.X * value.X + value.Y * value.Y);
                var normal = new Vector2(value.X * invLen, value.Y * invLen);
                Velocity = normal;
                figure.Velocity = -normal;
            }
        }
    }
}
