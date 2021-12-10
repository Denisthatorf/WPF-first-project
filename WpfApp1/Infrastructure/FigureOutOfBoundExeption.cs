using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.Infrastructure
{

    public class FigureOutOfBoundExeption : Exception
    {
        public FigureOutOfBoundExeption() : base() { }
        public FigureOutOfBoundExeption(string message) : base(message) { }
        public FigureOutOfBoundExeption(string message, Exception e) : base(message, e) { }
        //If there is extra error information that needs to be captured
        //create properties for them.

        private float _dx;

        public float dX
        {
            get { return _dx; }
            set { _dx = value; }
        }
        private float _dy;

        public float dY
        {
            get { return _dy; }
            set { _dy = value; }
        }



    }
}
