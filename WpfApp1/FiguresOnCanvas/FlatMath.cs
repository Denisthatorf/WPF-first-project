using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;

namespace WpfApp1.FiguresOnCanvas
{
    public sealed class FlatMath
    {
        public static double Distance(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static void GetInterval(TriangleOnCanvas a, Vector2 xAxis, out float min0, out float max0)
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
        public static bool IntervalIntersect(Figure a, Figure b, Vector2 xAxis, Vector2 offset)
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
    }
}
