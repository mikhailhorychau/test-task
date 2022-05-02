namespace TestTask.UserInteraction;

public interface IUserInteraction
{
    public IUserInteraction Clear();
    public IUserInteraction AddText(string text);
    public IUserInteraction AddSelectionOption(string name, Action? onSelect = null);

    public IUserInteraction AddInputOption(string name, bool isNumeric = false, Action<string>? onValueChanged = null,
        string initial = "");
}