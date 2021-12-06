using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using WpfApp1.Infrastructure;

namespace WpfApp1.FiguresOnCanvas
{
    [Serializable]
    [XmlInclude(typeof(RectangleOnCanvas))]
    [XmlInclude(typeof(CircleOnCanvas))]
    [XmlInclude(typeof(TriangleOnCanvas))]


    [DataContract]
    [KnownType(typeof(RectangleOnCanvas))]
    [KnownType(typeof(TriangleOnCanvas))]
    [KnownType(typeof(CircleOnCanvas))]
    public abstract class Figure : PropChangeNotify
    {

        #region Properties and fields
        
        [NonSerialized]
        static protected Random random = new Random();
        [NonSerialized]
        static protected Point CurentWindowCurentXY;
        static public Point pMax
        {
            get { return CurentWindowCurentXY; }
            set { CurentWindowCurentXY = value; }
        }
        [NonSerialized]
        static protected readonly double MARGIN = 10;

        [DataMember]
        protected double width;
        [DataMember]
        protected double height;
        [DataMember]
        protected double mass;
        [DataMember]
        protected double radius;


        [DataMember]
        public bool IsMoving = true;
        [DataMember]
        protected double dY;
        [DataMember]
        protected double dX;
        [DataMember]
        protected double x;
        [DataMember]
        protected double y;
        [NonSerialized]
        public Vector2 Velocity;


        public double X { get => x; set { x = value; OnPropertyChanged(); } }
        public double Y { get => y; set { y = value; OnPropertyChanged(); } }
        public double CentreX { get { return X + Radius; } }
        public double CentreY { get { return Y + Radius; } }

        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public double Mass { get => mass; set => mass = value; }
        public double Radius { get => radius; set => radius = value; }

        #endregion

        public Figure()
        { 
            dX = random.NextDouble() * 20 - 10;
            dY = random.NextDouble() * 20 - 10;
            Velocity.X = (float)dX;
            Velocity.Y = (float)dY;

            FiguresRandomizer.FigureRandomizer.Hig_Wei_X_Y_pMax_Mar(ref height, ref width, ref x, ref y, pMax, MARGIN);        
        }


        public abstract bool IsCollide(Figure figure);
        public abstract void OnCollision(Figure figure);


        public virtual void Move()
        {
            if (IsMoving)
            {
                //Collision with top and bottom
                if (Y + Height + MARGIN > pMax.Y
                          || Y < MARGIN)
                {
                    // dX = dX;
                    dY = -dY;
                    Velocity.Y = -Velocity.Y;
                }

                //Collision with left and right
                if (X + Width + MARGIN > pMax.X
                         || X - MARGIN < 0)
                {
                    //dX = -dX;
                    Velocity.X = -Velocity.X;
                    //dY = dY;
                }

                X += Velocity.X;
                Y += Velocity.Y;
            }
        }
        public virtual void Move(Vector2 vector)
        {
            X += vector.X;
            Y += vector.Y;
        }


        [OnDeserialized()]
        protected void DesesializedFunc(StreamingContext context)
        {
            Velocity.X = (float)dX;
            Velocity.Y = (float)dY;
        }



    }
}
