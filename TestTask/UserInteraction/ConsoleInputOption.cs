namespace TestTask.UserInteraction;

public class ConsoleInputOption : ConsoleOption
{
    public readonly bool IsNumeric;
    
    private string _value = "";
    private readonly Action<string>? _valueChangedActionAction;

    public string Value
    {
        get => _value;
        set
        {
            if (_value == value) return;

            _value = value;
            
            _valueChangedActionAction?.Invoke(_value);
        }
    }

    public ConsoleInputOption(
        int position, 
        string text, 
        bool isNumeric = false, 
        Action<string>? valueChangedAction = null,
        string initial = "") : base(position, text, null)
    {
        _valueChangedActionAction = valueChangedAction;
        IsNumeric = isNumeric;
        Value = initial;
    }
    
    public override string ToString() => $"{Text} {_value}";

    public void AddChar(char ch)
    {
        if (Value == "0")
            Value = ch.ToString();
        else
            Value += ch;
    }
}
