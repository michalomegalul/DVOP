using System;

namespace DVOPz.Module.Math;

public class Circle
{
    public double Diameter { get; set; }
    public const double PI = 3.1415926535897931;
    public double Perimeter { get; set; }
    public double Area { get; set; }

    public Circle(double diameter)
    {
        Diameter = diameter;
    }

    public void CalculatePerimeter()
    {
        Perimeter = 2 * (Diameter / 2) * PI;
    }

    public void CalculateArea()
    {
        Area = ((Diameter / 2) * (Diameter / 2)) * PI;
    }

    public void Clean()
    {
        Circle.ReferenceEquals(this, null);
    }
}
