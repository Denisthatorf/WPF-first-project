
using System;
using System.Runtime.Serialization;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [DataContract]
    public class CircleOnCanvas : Figure
    {
        public CircleOnCanvas() : base()
        {
        }

        public override void InitiolizeShape()
        {
            /*Width = Height = random.NextDouble() * 30 + 10;
            X = random.NextDouble() * (pMax.X - Height - MARGIN) + MARGIN;
            Y = random.NextDouble() * (pMax.Y - Height - MARGIN) + MARGIN;*/
            FiguresRandomizer.FigureRandomizer.Hig_Wei_X_Y_pMax_Mar(ref height, ref width, ref x, ref y, pMax, MARGIN);
        }
    }
}
