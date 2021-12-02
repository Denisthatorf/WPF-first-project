using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FiguresRandomizer
{

        public static class FigureRandomizer
        {
            private static Random random = new Random();
           
            static public void Hig_Wei_X_Y_pMax_Mar(ref double width, ref double height, ref double X, ref double Y, Point pMax, double MARGIN)
            {
                width = height = random.NextDouble() * 20 + 10;

                X = random.NextDouble() * (pMax.X - width - MARGIN) + MARGIN;
                Y = random.NextDouble() * (pMax.Y - height - MARGIN) + MARGIN;
            }
        }
}
