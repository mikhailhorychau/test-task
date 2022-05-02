namespace TestTask.UserInteraction;

public class ConsoleOption : ConsoleElement
{
    private readonly Action? _onSelectAction;
    
    public ConsoleOption(int position, string text, Action? onSelectAction) : base(position, text)
    {
        _onSelectAction = onSelectAction;
    }

    public void Select()
    {
        _onSelectAction?.Invoke();
    }
}