using TestTask.Extensions;
using TestTask.Figures.Exceptions;

namespace TestTask.Figures;

public class Parallelogram : Figure
{
    public readonly int SideA;
    public readonly int SideB;
    public readonly int AngleA;
    public readonly int AngleB;

    public Parallelogram(int sideA, int sideB, int angleA, int angleB)
    {
        SideA = sideA;
        SideB = sideB;
        AngleA = angleA;
        AngleB = angleB;
        
        ValidateAngles();
        ValidateSides();
    }

    public override double Perimeter() => (SideA + SideB) * 2;

    public override double Area() 
        => Math.Round(SideA * SideB * Math.Sin(AngleA.ToRadians()));


    private void ValidateSides()
    {
        if (SideA > 0 && SideB > 0) return;

        throw new InvalidSideException();
    }

    private void ValidateAngles()
    {
        if (AngleA > 0 && AngleB > 0 && AngleA + AngleB == 180) return;

        throw new InvalidAngleException();
    }
}
