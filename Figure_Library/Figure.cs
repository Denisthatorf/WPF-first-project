using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figure_Library
{
    public abstract class Figure
    {

        static protected Point CurentWindowCurentXY;
        static public Point pMax
        {
            get { return CurentWindowCurentXY; }
            set { CurentWindowCurentXY = value; }
        }

        public double dY;
        public double dX;

        protected Shape _shape;
        public Shape ClassShape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        protected Figure(ref Canvas canvas)
        {

            Random random = new Random();

            dX = random.NextDouble() * 20 - 10;
            dY = random.NextDouble() * 20 - 10;

            InitiolizeShape(ref canvas);
        }

        public abstract void InitiolizeShape(ref Canvas canvas);
        public virtual void Move()
        {

            //Collision with top and bottom
            if (Canvas.GetTop(ClassShape) + ClassShape.Height + 8 > pMax.Y
                      || Canvas.GetTop(ClassShape) - 8 < 0)
            {
                // dX = dX;
                dY = -dY;
            }

            //Collision with left and right
            if (Canvas.GetLeft(ClassShape) + ClassShape.Width + 8 > pMax.X
                     || Canvas.GetLeft(ClassShape) - 8 < 0)
            {
                dX = -dX;
               // dY = dY;
            }



            Canvas.SetLeft(ClassShape, Canvas.GetLeft(ClassShape) + dX);
            Canvas.SetTop(ClassShape, Canvas.GetTop(ClassShape) + dY);

        }
    }
}
