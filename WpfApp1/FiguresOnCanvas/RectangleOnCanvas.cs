using System;
using System.Runtime.Serialization;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class RectangleOnCanvas : Figure
    {
        public RectangleOnCanvas() : base()
        {
        }


        public override void InitiolizeShape()
        {
             
           /* Width= Height = random.NextDouble() * 20 + 10;

            X = random.NextDouble() * (pMax.X - Width - MARGIN) + MARGIN;
            Y = random.NextDouble() * (pMax.Y - Height - MARGIN) + MARGIN;*/

            FiguresRandomizer.FigureRandomizer.Hig_Wei_X_Y_pMax_Mar(ref height, ref width, ref x, ref y, pMax, MARGIN);

        }

        public override bool IsCollide(Figure figure)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(Figure figure)
        {
            throw new NotImplementedException();
        }
    }
}
