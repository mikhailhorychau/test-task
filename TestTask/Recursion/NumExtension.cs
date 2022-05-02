namespace TestTask.Recursion;

public static class NumExtension
{
    public static int Pow(int value, int pow)
    {
        if (pow == 0)
            return 1;
        
        if (pow == 1)
            return value;

        return Pow(value, pow - 1) * value;
    }
    
    public static int Fibonacci(int index)
    {
        if (index <= 2)
            return 1;

        return Fibonacci(index - 1) + Fibonacci(index - 2);
    }
}