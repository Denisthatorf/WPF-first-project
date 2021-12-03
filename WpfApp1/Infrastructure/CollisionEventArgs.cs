using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.Infrastructure
{
    public class CollisionEventArgs: EventArgs
    {
        public Type Type1 { get; }
        public Type Type2 { get; }

        public CollisionEventArgs(Type type1, Type type2)
        {
            Type1 = type1;
            Type2 = type2;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
