
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
                    new Point { X = 0, Y = 2 * Modifire },
                    new Point { X = 2 * Modifire , Y = 2 * Modifire },
                    new Point { X = 1 * Modifire , Y = 0}
                };
        }
        #region IsCollide
        public override bool IsCollide(Figure figure)
        {
            if (!(figure is TriangleOnCanvas)
                                    || figure == this || figure == null) return false;
            if (this.X + Width < figure.X || this.X > figure.X + Width) return false;
            if (this.Y + Width < figure.Y || this.Y > figure.Y + Height) return false;
            

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
                if (!FlatMath.IntervalIntersect(this, figure, xAxis, offset))
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
                if (!FlatMath.IntervalIntersect(this, b, xAxis, offset))
                    return false;
            }

            //Если ни одна из осей не нашла не пересекающихся проекций, то очевидно по теореме
            //многоугольники пересекаются - возвращаем тру.
            return true;
        }

        public override Point PointOfCollision(Figure figure)
        {
            throw new NotImplementedException();
            /*int poitIndex = -1;
            double minDistance = float.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                Point v = (figure as TriangleOnCanvas).ListOfPoints[i];                
                double distance = FlatMath.Distance(v, new Point(CentreX, CentreY));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    poitIndex = i;
                }
            }

            var PointRes = (figure as TriangleOnCanvas).ListOfPoints[poitIndex];
            return new Point(figure.X + PointRes.X, figure.Y - PointRes.Y);*/
        }
        #endregion

    }
}
