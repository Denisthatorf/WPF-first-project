using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Figure_Library
{
    public class TriangleOnCanvas : Figure
    {
        TriangleOnCanvas(ref Canvas canvas) : base(ref canvas) { }

        public override void InitiolizeShape(ref Canvas canvas)
        {
            Random random = new Random();

            double Modifire = random.NextDouble() * (30 - 10) + 10;
            double height = Modifire * 2;  double width = height;

            double X = random.NextDouble() * (pMax.X - 200 - Modifire * 3 - 10);
            double Y = random.NextDouble() * (pMax.Y - 75 - Modifire * 3 - 10);

            var listOfPoint = new PointCollection
                {
                    new Point { X = 1 * Modifire , Y = 0},
                    new Point { X = 2 * Modifire , Y = 2 * Modifire },
                    new Point { X = 0, Y = 2 * Modifire }
                };

            var Triangle = new Polygon
            {
                Points = listOfPoint,
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Fill = Brushes.Aqua,
            };

            Canvas.SetLeft(Triangle, X);
            Canvas.SetTop(Triangle, Y);

            //Connect this obj with the figure that will be display on the screen
            ClassShape = Triangle;
            canvas.Children.Add(ClassShape);

            
        }
    }
}
