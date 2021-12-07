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
        }

        public override bool IsCollide(Figure figure)
        {
            if (figure!=null && figure is RectangleOnCanvas && figure != this)
            {
                if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
                if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;

                return true;
            }
            return false;
        }

        public override void OnCollision(Figure figure)
        {
            //formula of normal
            var n = new Vector2(
                (float)(figure.CentreX - this.CentreX),
                (float)(figure.CentreY - this.CentreY));

            //Vector2 rv = figure.Velocity - this.Velocity;

            float invLen = 1f / MathF.Sqrt(n.X * n.X + n.Y * n.Y);
            var un = new Vector2(n.X * invLen, n.Y * invLen);
            var ut = new Vector2(-un.Y, un.X);

            var V1n = Vector2.Dot(un, this.Velocity);
            var V1t = Vector2.Dot(ut, this.Velocity);

            var V2n = Vector2.Dot(un, figure.Velocity);
            var V2t = Vector2.Dot(ut, figure.Velocity);

            var V1n_AfterCollision = (V1n * (this.Mass - figure.Mass) + 2 * figure.Mass * V2n)
                                       / (this.Mass + figure.Mass);
            var V2n_AfterCollision = (V2n * (figure.Mass - this.Mass) + 2 * this.Mass * V1n)
                                       / (this.Mass + figure.Mass);


            this.Move(-un);
            figure.Move(un);

            Vector2 v1n = (float)V1n * -un;
            Vector2 v1t = (float)V1t * ut;
            Vector2 v2n = (float)V2n * -un;
            Vector2 v2t = (float)V2t * ut;

            this.Velocity = v1n + v1t;
            figure.Velocity = v2n + v2t;
        }
    }
}
