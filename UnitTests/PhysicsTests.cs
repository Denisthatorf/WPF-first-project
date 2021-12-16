using System;
using WpfApp1.FiguresOnCanvas;
using Xunit;

namespace UnitTests
{
    public class PhysicsTests
    {
       
        [Theory]
        [InlineData (2,2,1,
                     4,2,1)]
        public void CirleVsCircleTrueTest( double f1radius, double f1X, double f1Y,
             double f2radius, double f2X, double f2Y)
        {
            CircleOnCanvas figure1 = new CircleOnCanvas() 
            {
                 Radius = f1radius,
                 X = f1X,
                 Y = f1Y
            };
            CircleOnCanvas figure2 = new CircleOnCanvas() 
            {
                Radius = f2radius,
                X = f2X,
                Y = f2Y
            };


            Assert.True(figure1.IsCollide(figure2));
        }


        [Theory]
        [InlineData(2, 2, 1,
                     4, 7, 1)]
        public void CirleVsCircleFalseTest(double f1radius, double f1X, double f1Y,
                                           double f2radius, double f2X, double f2Y)
        {
            CircleOnCanvas figure1 = new CircleOnCanvas()
            {
                Radius = f1radius,
                X = f1X,
                Y = f1Y
            };
            CircleOnCanvas figure2 = new CircleOnCanvas()
            {
                Radius = f2radius,
                X = f2X,
                Y = f2Y
            };


            Assert.False(figure1.IsCollide(figure2));
        }

        [Theory]
        [InlineData(2,2,2,2,
                    2,2,4,3)]
        [InlineData(2, 2, 2, 2,
                    3, 3, 2, 3)]
        [InlineData(2, 2, 2, 2,
                   2, 2, 4, 4)]
        public void AABBvsAABBTrueTests(double r1_width, double r1_height, double r1_x, double r1_y, double r2_width, double r2_height, double r2_x, double r2_y)
        {
            RectangleOnCanvas rec1 = new RectangleOnCanvas()
            {
                Width = r1_width,
                Height = r1_height,
                X = r1_x,
                Y = r1_y
            };
            RectangleOnCanvas rec2 = new RectangleOnCanvas()
            {
                Width = r2_width,
                Height = r2_height,
                X = r2_x,
                Y = r2_y
            };

            Assert.True(rec1.IsCollide(rec2));
        }

        [Theory]
        [InlineData(2, 2, 2, 2,
                   2, 2, 4, 4.1)]
        [InlineData(2, 2, 2, 2,
                   3, 3, 6, 6)]
        public void AABBvsAABBFalseTests(double r1_width, double r1_height, double r1_x, double r1_y, double r2_width, double r2_height, double r2_x, double r2_y)
        {
            RectangleOnCanvas rec1 = new RectangleOnCanvas()
            {
                Width = r1_width,
                Height = r1_height,
                X = r1_x,
                Y = r1_y
            };
            RectangleOnCanvas rec2 = new RectangleOnCanvas()
            {
                Width = r2_width,
                Height = r2_height,
                X = r2_x,
                Y = r2_y
            };

            Assert.False(rec1.IsCollide(rec2));
        }
    }
}
