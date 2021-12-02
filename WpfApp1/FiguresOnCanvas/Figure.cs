﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
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

        public double Width { get => width; set { width = value; OnPropertyChanged(); } }
        public double Height { get => height; set { height = value; OnPropertyChanged(); } }
        public double X { get => x; set { x = value; OnPropertyChanged(); } }
        public double Y { get => y; set { y = value; OnPropertyChanged(); } }
        #endregion

        public Figure()
        {
            InitiolizeShape();

            //vector speed (-10...10 ; -10...10)
            dX = random.NextDouble() * 20 - 10;
            dY = random.NextDouble() * 20 - 10;
        }

        public abstract void InitiolizeShape();
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
                }

                //Collision with left and right
                if (X + Width + MARGIN > pMax.X
                         || X - MARGIN < 0)
                {
                    dX = -dX;
                    //dY = dY;
                }

                /* Canvas.SetLeft(ClassShape, Canvas.GetLeft(ClassShape) + dX);
                 Canvas.SetTop(ClassShape, Canvas.GetTop(ClassShape) + dY);*/

                X += dX;
                Y += dY;
            }
        }
    }
}