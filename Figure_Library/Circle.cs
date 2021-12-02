using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Figure_Library
{
    public class CircleOnCanvas : Figure
    {
        CircleOnCanvas(ref Canvas canvas) : base(ref canvas) { }
        public override void InitiolizeShape(ref Canvas canvas)
        {
            Random random = new Random();

            double radius = random.NextDouble() * (50 - 10) + 10;
            double X = random.NextDouble() * (pMax.X - radius * 2 - 10);
            double Y = random.NextDouble() * (pMax.Y - radius * 2 - 10);

            Ellipse circle = new Ellipse()
            {
                Width = radius,
                Height = radius,
                Fill = Brushes.Aqua,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(circle, X);
            Canvas.SetTop(circle, Y);

            ClassShape = circle;
            canvas.Children.Add(ClassShape);
        }
    }
}
