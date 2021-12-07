
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class TriangleOnCanvas : Figure
    {

        [DataMember]
        private List<Point> _listOfPoints;

        public List<Point> ListOfPoints
        {
            get { return _listOfPoints; }
            set { _listOfPoints = value; }
        }
        public TriangleOnCanvas() : base()
        {
            Radius = Width / Math.Sqrt(3);
            double Modifire = Width/2;

            ListOfPoints = new List<Point>
                {
                    new Point { X = 1 * Modifire , Y = 0},
                    new Point { X = 2 * Modifire , Y = 2 * Modifire },
                    new Point { X = 0, Y = 2 * Modifire }
                };
        }
        #region IsCollide
        public override bool IsCollide(Figure figure)
        {
            if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
            if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;
            if (!(figure is TriangleOnCanvas)
                                    || figure == this || figure == null) return false;

            //Вычисляем отступ между многоугольниками для приведения к центру координат
            Vector2 offset = Velocity - figure.Velocity;

            //В данной переменной храним ось на которую будет проецировать
            Vector2 xAxis = Vector2.Zero;

            //Проходим по всем сторонам первого многоугольника и используем их как ось
            for (int j = 2, i = 0; i < 3; j = i, i++)
            {
                //Вычисляем вектор стороны многоугольника
                Vector2 E = new Vector2((float)(this.ListOfPoints[i].X - this.ListOfPoints[j].X ),
                                        (float)(this.ListOfPoints[i].Y - this.ListOfPoints[j].Y));
                //Вычисляем вектор нормали к стороне многоугольника
                xAxis = new Vector2(-E.Y, E.X);

                //Если проекции многоугольников не пересекаются - то по теореме многоугольники не пересекаются
                if (!IntervalIntersect(this, figure, xAxis, offset))
                    return false;
            }

            var b = figure as TriangleOnCanvas;
            //Если пока не нашли нужной оси - проходим по всем сторонам второго многоугольника
            for (int j = 2, i = 0; i < 3; j = i, i++)
            {
                //Вычисляем вектор стороны и нормаль
                Vector2 E = new Vector2((float)(b.ListOfPoints[i].X - b.ListOfPoints[j].X),
                                        (float)(b.ListOfPoints[i].Y - b.ListOfPoints[j].Y));
                xAxis = new Vector2(-E.Y, E.X); //Getting normal vector

                //И проверяем на пересечение проекций
                if (!IntervalIntersect(this, b, xAxis, offset))
                    return false;
            }

            //Если ни одна из осей не нашла не пересекающихся проекций, то очевидно по теореме
            //многоугольники пересекаются - возвращаем тру.
            return true;
        }

        private static bool IntervalIntersect(Figure a, Figure b, Vector2 xAxis, Vector2 offset)
        {
            float min0, max0, min1, max1; //проекции на ось первого и второго многоугольника
            GetInterval(a as TriangleOnCanvas, xAxis, out min0, out max0);
            //получаем первую проекцию
            GetInterval(b as TriangleOnCanvas, xAxis, out min1, out max1);
            //получаем вторую проекцию

            float h = Vector2.Dot(offset, xAxis); //получаем проекцию отступа 
            min0 += h; max0 += h; // и добавляем её к первому многоугольнику

            float d0 = min0 - max1;
            float d1 = min1 - max0;

            //если проекции не пересекаются то минимум первого больше максимума второго или
            //минимум второго больше максимума первого.
            if (d0 > 0.0f || d1 > 0.0f)
                return false;
            else return true;
        }

        private static void GetInterval(TriangleOnCanvas a, Vector2 xAxis, out float min0, out float max0)
        {
            min0 = max0 = Vector2.Dot(new Vector2((float)a.ListOfPoints[0].X, (float)a.ListOfPoints[0].Y), xAxis);
            //Проходимся по всем вершинам и проецируем их на ось
            for (int i = 1; i < 3; i++)
            {
                //проецируем вершину на ось
                float dot = Vector2.Dot(new Vector2((float)a.ListOfPoints[i].X, (float)a.ListOfPoints[i].Y), xAxis);
                //Устанавливаем минимум и максимум
                if (dot < min0)
                    min0 = dot;
                else if (dot > max0)
                    max0 = dot;
            }
        }
        #endregion

    }
}
