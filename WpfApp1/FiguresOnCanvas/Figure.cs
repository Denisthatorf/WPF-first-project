﻿using System;
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
            dX = random.NextDouble() * 2 - 0.5;
            dY = random.NextDouble() * 2 - 0.5;
            Velocity.X = (float)dX;
            Velocity.Y = (float)dY;

            FiguresRandomizer.FigureRandomizer.Hig_Wei_X_Y_pMax_Mar(ref height, ref width, ref x, ref y, pMax, MARGIN);        
        }


        public abstract bool IsCollide(Figure figure);
        public virtual void OnCollision(Figure figure)
        {
            #region Link on documentation
            //https://www.vobarian.com/collisions/2dcollisions2.pdf
            #endregion


            //formula of normal
            var n = new Vector2(
                (float)(figure.CentreX - this.CentreX),
                (float)(figure.CentreY - this.CentreY));

            //Vector2 rv = figure.Velocity - this.Velocity;

            float invLen = 1f / MathF.Sqrt(n.X * n.X + n.Y * n.Y);
            var un = new Vector2(n.X * invLen, n.Y * invLen);
            var ut = new Vector2(-un.Y, un.X);

            var V1n = Vector2.Dot(un, this.Velocity);
            var V1t = Vector2.Dot(ut, this.Velocity);

            var V2n = Vector2.Dot(un, figure.Velocity);
            var V2t = Vector2.Dot(ut, figure.Velocity);

            var V1n_AfterCollision = (V1n * (this.Mass - figure.Mass) + 2 * figure.Mass * V2n)
                                       / (this.Mass + figure.Mass);
            var V2n_AfterCollision = (V2n * (figure.Mass - this.Mass) + 2 * this.Mass * V1n)
                                       / (this.Mass + figure.Mass);


           this.Move(-un);
           figure.Move(un);

            Vector2 v1n = (float)V1n * -un;
            Vector2 v1t = (float)V1t * ut;
            Vector2 v2n = (float)V2n * -un;
            Vector2 v2t = (float)V2t * ut;

            this.Velocity = v1n + v1t;
            figure.Velocity = v2n + v2t;
        }

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
