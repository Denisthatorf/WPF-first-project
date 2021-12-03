
using System;
using System.Numerics;
using System.Runtime.Serialization;
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
        }

        public override void InitiolizeShape()
        {
            /*Width = Height = random.NextDouble() * 30 + 10;
            X = random.NextDouble() * (pMax.X - Height - MARGIN) + MARGIN;
            Y = random.NextDouble() * (pMax.Y - Height - MARGIN) + MARGIN;*/
            FiguresRandomizer.FigureRandomizer.Hig_Wei_X_Y_pMax_Mar(ref height, ref width, ref x, ref y, pMax, MARGIN);
        }
        public override bool IsCollide(Figure figure)
        {
            if (figure is CircleOnCanvas &&
                figure!=this)
            {
                double r = this.Width + figure.Width;
                r *= r;
                return r > Math.Pow(this.X - figure.X, 2) + Math.Pow(this.Y- figure.Y, 2) +2;
            }
            else { return false; }
        }

        public override void OnCollision(Figure figure)
        {
            if (IsCollide(figure))
            {
                var A = this;
                var B = figure as CircleOnCanvas;

                // Вычисляем относительную скорость
                Vector2 rv = B.Velocity - A.Velocity;

                //Вычисляем относительную скорость относительно направления нормали
               //float velAlongNormal = DotProduct(rv, normal)

                 // Не выполняем вычислений, если скорости разделены
              //  if (velAlongNormal > 0)
               //     return;

                // Вычисляем упругость
               // float e = min(A.restitution, B.restitution)

               // Вычисляем скаляр импульса силы
                //float j = -(1 + e) * velAlongNormal
               // j /= 1 / A.mass + 1 / B.mass

  // Прикладываем импульс силы
                A.Velocity.X = -A.Velocity.X ;
                A.Velocity.Y = -A.Velocity.Y ;
                B.Velocity.X = -B.Velocity.X;
                B.Velocity.Y = -B.Velocity.Y;

               // Vector2 impulse = j * normal;
               //A.velocity -= 1 / A.mass * impulse
               // B.velocity += 1 / B.mass * impulse
            }
        }
    }
}
