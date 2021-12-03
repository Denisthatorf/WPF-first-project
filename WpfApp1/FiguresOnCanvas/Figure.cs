using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
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
        public bool IsMoving = true;
        [DataMember]
        protected double dY;
        [DataMember]
        protected double dX;
        [DataMember]
        protected double width;
        [DataMember]
        protected double height;
        [DataMember]
        protected double x;
        [DataMember]
        protected double y;
        [DataMember]
        private double _mass;
        [NonSerialized]
        protected Vector2 Velocity;
        [NonSerialized]
        protected Shape _classShape;

        public Shape ClassShape {get { return _classShape; } set { _classShape = value; }}
        public double Mass { get => _mass; set { _mass = value; OnPropertyChanged(); } }
        public double Width { get => width; set { width = value; OnPropertyChanged(); } }
        public double Height { get => height; set { height = value; OnPropertyChanged(); } }
        public double X { get => x; set { x = value; OnPropertyChanged(); } }
        public double Y { get => y; set { y = value; OnPropertyChanged(); } }
        #endregion

        public Figure()
        {
            InitiolizeShape();

            //vector speed (-10...10 ; -10...10)
            dX = random.NextDouble() * 5 - 1;
            dY = random.NextDouble() * 5 - 1;
            Velocity.X = (float)dX;
            Velocity.Y = (float)dY;

        }

        public abstract void InitiolizeShape();
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

        [OnDeserialized()]
        protected void DesesializedFunc(StreamingContext context)
        {
            Velocity.X = (float)dX;
            Velocity.Y = (float)dY;
        }
    }
}
