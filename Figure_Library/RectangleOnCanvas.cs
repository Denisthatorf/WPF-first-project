using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Figure_Library
{
    public class RectangleOnCanvas : Figure
    {
        RectangleOnCanvas(ref Canvas canvas): base(ref canvas) { }
        public override void InitiolizeShape(ref Canvas canvas)
        {

            Random random = new Random();

            double width = random.NextDouble() * (30 - 10) + 10;
            double height = random.NextDouble() * (30 - 10) + 10;


            double X = random.NextDouble() * (pMax.X - width * 3 - 10);
            double Y = random.NextDouble() * (pMax.Y - height * 3 - 10);

            Rectangle rect = new Rectangle()
            {
                Width = width,
                Height = height,
                Fill = Brushes.Aqua,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);


            ClassShape = rect;
            canvas.Children.Add(ClassShape);

        }
    }
}
