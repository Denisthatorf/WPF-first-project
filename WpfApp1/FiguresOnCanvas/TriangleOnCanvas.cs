
using System;
using System.Collections.Generic;
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
        }
        
        public override void InitiolizeShape()
        {
            double Modifire = random.NextDouble() * 20 + 10;

            X = random.NextDouble() * (pMax.X  - Height  -  MARGIN) + MARGIN;
            Y = random.NextDouble() * (pMax.Y - Height -  MARGIN) + MARGIN;
          

            Height = Width = Modifire * 2;
            ListOfPoints = new List<Point>
                {
                    new Point { X = 1 * Modifire , Y = 0},
                    new Point { X = 2 * Modifire , Y = 2 * Modifire },
                    new Point { X = 0, Y = 2 * Modifire }
                };
        }

        public override bool IsCollide(Figure figure)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(ref Figure figure)
        {
            throw new NotImplementedException();
        }
    }
}
