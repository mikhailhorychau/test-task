namespace TestTask.Figures;

public class Rectangle : Parallelogram
{
    public Rectangle(int sideA, int sideB) : base(sideA, sideB, 90, 90)
    {
        
    }
//if we care about performance, just uncomment the line below
//    public override double Area() => SideA * SideB;
}