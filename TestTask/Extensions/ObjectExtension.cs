namespace TestTask.Extensions;

public static class ObjectExtension
{
    public static TObject IfThen<TObject>(this TObject obj, bool condition, Action<TObject> action)
    {
        if (condition)
            action?.Invoke(obj);

        return obj;
    }
}