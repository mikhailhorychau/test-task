using TestTask.Extensions;
using TestTask.Figures.Exceptions;

namespace TestTask.Figures;

public class Circle : Figure
{
    public double Radius { get; }

    public Circle(double radius)
    {
        Radius = radius;
        
        ValidateRadius();
    }

    public override double Perimeter() => 2 * Radius * Math.PI;

    public override double Area() => Radius * Radius * Math.PI;

    private void ValidateRadius()
    {
        if (Radius.GreatThenZero()) return;

        throw new InvalidRadiusException();
    }
}