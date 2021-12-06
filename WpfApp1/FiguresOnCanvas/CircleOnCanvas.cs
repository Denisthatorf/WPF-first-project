
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
        }

        public override bool IsCollide(Figure figure)
        {
            if (figure is CircleOnCanvas &&
                figure!=this)
            {
                double r = this.Radius + figure.Radius;
                r *= r;
                return r + 1 > Math.Pow(this.CentreX - figure.CentreX, 2) + Math.Pow(this.CentreY - figure.CentreY, 2) ;
            }
            else { return false; }
        }

        public override void OnCollision(Figure figure)
        {
            var value = figure.Velocity;
            var rv = this.Velocity - figure.Velocity;
            float invLen = 1f / MathF.Sqrt(value.X * value.X + value.Y * value.Y);
            var normal = new Vector2(value.X * invLen, value.Y * invLen);

            Velocity = normal;
            figure.Velocity = -normal;

            // Calculate relative velocity
            /*
                        Vector2 rv = this.Velocity - figure.Velocity;

                        float velAlongNormal = this.Velocity.X*figure.Velocity.X + this.Velocity.Y*figure.Velocity.Y;

                        if (velAlongNormal > 0)
                            return;

                        //float e = min(A.restitution, B.restitution);
                        float j = - velAlongNormal;
                        // j /= 1 / this.Mass + 1 / figure.Mass;
                        // Apply impulse
                        float invLen = 1f / MathF.Sqrt((float)(this.X * this.X + this.Y * this.Y));
                        var normal = new Vector2(this.Velocity.X * invLen, figure.Velocity.Y * invLen);
                        Vector2 impulse = j * normal;
                        this.Velocity -= impulse;
                        figure.Velocity += impulse;
            */


        }
    }
}
