namespace TestTask.UserInteraction;

public class ConsoleElement
{
    public int Position;
    public readonly string Text;

    public ConsoleElement(int position, string text)
    {
        Position = position;
        Text = text;
    }
    
    public override string ToString()
    {
        return Text;
    }
}