using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;

namespace WpfApp1.FiguresOnCanvas
{
    public interface ICollision
    {
        bool IsCollide(Shape shape);
    }
}
