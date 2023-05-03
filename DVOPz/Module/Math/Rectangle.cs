using System;

namespace DVOPz.Module.Math;

public class Rectangle
{
    public int Width { get; }
    public int Height { get; }
    public int Area { get; private set; }
    public int Perimeter { get; private set; }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void CalculateArea()
    {
        Area = Width * Height;
    }

    public void CalculatePerimeter()
    {
        Perimeter = 2 * (Width + Height);
    }

    public void Clean()
    {
        Rectangle.ReferenceEquals(this, null);
    }
}