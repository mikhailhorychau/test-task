namespace TestTask.Arrays;

public static class ArraysExtension
{
    public static int DiagonalSum(this int[][] array) 
        => array.Select((t, index) => t[index]).Sum();

    public static int MultipleOfThreeSum(this int[][] array) 
        => array.Sum(values => values.MultipleOfThreeSum());

    public static int MultipleOfThreeSum(this IEnumerable<int> array) 
        => array.Where(value => value % 3 == 0).Sum();

    public static string ToFormatString(this int[][] array)
        => array.Aggregate("", (current, value) => current + value.ToFormatString() + "\n");

    public static string ToFormatString(this IEnumerable<int> array) 
        => array.Aggregate("", (current, value) => current + $"{value :00} ");

    public static int[][] RandomArray(int count)
    {
        var random = new Random();
        var arr = new int[count][];

        for (var i = 0; i < count; i++)
        {
            arr[i] = new int[count];
            for (var j = 0; j < count; j++)
            {
                arr[i][j] = random.Next(0, 100);
            }
        }

        return arr;
    }
}