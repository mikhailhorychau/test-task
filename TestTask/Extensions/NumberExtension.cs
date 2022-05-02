namespace TestTask.Extensions;

public static class NumberExtension
{
    public static bool GreatThenZero(this double number) => number > 0;

    public static int ToIntOrZero(this string str)
    {
        try
        {
            return int.Parse(str);
        }
        catch (Exception e)
        {
            return 0;
        }
    }

    public static double ToRadians(this int degrees) => 3.14 / 180 * degrees;
}