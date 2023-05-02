using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOPz.Module.Math;

public class Rectangle
{
    public int Width { get; }
    public int Height { get; }
    public int Area { get; private set; }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public void CalculateArea()
    {
        Area = Width * Height;
    }
}
