using TestTask.Utils;

namespace TestTask.UserInteraction;

public class ConsoleInteraction : IUserInteraction
{
    private Vector2 _cursorPosition = new Vector2(0, 0);
    private bool _isLooping = false;

    private readonly List<ConsoleElement> _elements = new List<ConsoleElement>();
    private readonly List<ConsoleOption> _options = new List<ConsoleOption>();

    private int _freePosition = 0;
    private int _currentOptionIndex = 0;

    private ConsoleOption? _currentOption = null;

    public IUserInteraction Clear()
    {
        Console.Clear();
        
        _currentOption = null;
        _freePosition = 0;
        _currentOptionIndex = 0;
        _elements.Clear();
        _options.Clear();
        
        SetCursorPosition(_cursorPosition);
        
        return this;
    }

    public IUserInteraction AddText(string text)
    {
        _elements.Add(new ConsoleElement(_freePosition++, text));
        
        return this;
    }

    public IUserInteraction AddSelectionOption(string name, Action? onSelect = null)
    {
        var option = new ConsoleOption(_freePosition++, name, onSelect);
        
        _elements.Add(option);
        AddOption(option);
        
        return this;
    }

    public IUserInteraction AddInputOption(string name, bool isNumeric, Action<string>? onValueChanged, string initial)
    {
        var inputOption = new ConsoleInputOption(_freePosition++, name, isNumeric, onValueChanged, initial);
        
        _elements.Add(inputOption);
        AddOption(inputOption);
        
        return this;
    }
    

    public void StartLoop()
    {
        _isLooping = true;
        
        Draw();
        HideCursor();

        ConsoleKeyInfo keyInfo;
        
        while (_isLooping)
        {
            keyInfo = ReadInput();
            ChangeInputOption(keyInfo);
            Navigation(keyInfo);
            Draw();
        }
    }

    private static void HideCursor() => Console.CursorVisible = false;

    private void AddOption(ConsoleOption option)
    {
        _options.Add(option);
        
        if (_currentOption == null)
            _currentOption = option;
    }

    private ConsoleKeyInfo ReadInput() => Console.ReadKey(true);

    private void ChangeInputOption(ConsoleKeyInfo keyInfo)
    {
        if (_currentOption is not ConsoleInputOption input) return;
        
        var keyChar = keyInfo.KeyChar;
        
        if (keyInfo.Key == ConsoleKey.Backspace)
            Backspace(input);

        if (IsValidInput(input, keyChar))
            input.AddChar(keyChar);
    }

    private static void Backspace(ConsoleInputOption input)
    {
        if (input.Value.Length == 0) return;
        input.Value = input.Value.Remove(input.Value.Length - 1);
        Console.Clear();
    }

    private static bool IsValidInput(ConsoleInputOption input, char keyChar)
    {
        if (input.IsNumeric)
            return char.IsDigit(keyChar);

        return char.IsLetterOrDigit(keyChar) || char.IsWhiteSpace(keyChar);
    }

    private void Navigation(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow:
                NextOption();
                break;
            case ConsoleKey.UpArrow:
                PrevOption();
                break;
            case ConsoleKey.Enter:
                SelectOption();
                break;
        }
    }
    
    private void NextOption()
    {
        if (_currentOptionIndex == _options.Count - 1)
            _currentOptionIndex = 0;
        else
            _currentOptionIndex++;

        _currentOption = _options[_currentOptionIndex];
    }

    private void PrevOption()
    {
        if (_currentOptionIndex == 0)
            _currentOptionIndex = _options.Count - 1;
        else
            _currentOptionIndex--;
        
        _currentOption = _options[_currentOptionIndex];
    }

    private void SelectOption()
    {
        if (_options.Count == 0) return;
        
        _options[_currentOptionIndex].Select();
    }

    private void Draw()
    {
        SetCursorPosition(_cursorPosition);
        
        foreach (var element in _elements)
        {
            Console.ForegroundColor = TextColor(element);
            
            if (_currentOption == element)
            {
                Console.BackgroundColor = Console.BackgroundColor = ConsoleColor.White;
            }
            
            if (element is ConsoleInputOption input)
            {
                Console.WriteLine(input);
                Console.ResetColor();

                continue;
            }
            
            Console.WriteLine(element);
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }

    private ConsoleColor TextColor(ConsoleElement element)
    {
        if (_options.Contains(element))
        {
            return element is ConsoleInputOption ? ConsoleColor.DarkGreen : ConsoleColor.DarkBlue;
        }

        return ConsoleColor.White;
    }

    private void SetCursorPosition(Vector2 vector) => Console.SetCursorPosition(vector.X, vector.Y);
}