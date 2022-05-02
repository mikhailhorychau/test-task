using TestTask.Utils;

namespace TestTask.UserInteraction;

public class ConsoleInteraction : IUserInteraction
{
    private Vector2 _cursorPosition = new Vector2(0, 0);
    private Vector2 _inputPosition = new Vector2(0, 0);
    private bool _isLooping = false;

    private readonly List<ConsoleElement> _elements = new List<ConsoleElement>();
    private readonly List<ConsoleOption> _options = new List<ConsoleOption>();
    private readonly List<ConsoleInputOption> _inputs = new List<ConsoleInputOption>();

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
        _inputs.Clear();
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
        _inputs.Add(inputOption);
        AddOption(inputOption);
        
        return this;
    }
    

    public void StartLoop()
    {
        _isLooping = true;
        Draw();
        Console.CursorVisible = false;
        while (_isLooping)
        {
            ReadInput();
            Draw();
        }
    }

    private void AddOption(ConsoleOption option)
    {
        _options.Add(option);
        
        if (_currentOption == null)
            _currentOption = option;
    }

    private void ReadInput()
    {
        var keyInfo = Console.ReadKey(true);
        if (_currentOption != null && _inputs.Contains(_currentOption))
        {
            var input = _currentOption as ConsoleInputOption;

            if (input == null) return;

            if (keyInfo.Key == ConsoleKey.Backspace && input.Value.Length > 0)
            {
                input.Value = input.Value.Remove(input.Value.Length - 1);
                Console.Clear();
            }

            if (input.IsNumeric)
            {
                if (char.IsDigit(keyInfo.KeyChar))
                    if (input.Value == "0")
                        input.Value = keyInfo.KeyChar.ToString();
                    else
                        input.Value += keyInfo.KeyChar;
            } 
            else if (char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsWhiteSpace(keyInfo.KeyChar))
            {
                input.Value += keyInfo.KeyChar;
            }
        }
        
        switch (keyInfo.Key)
        {
            case ConsoleKey.DownArrow : NextOption();
                break;
            case ConsoleKey.UpArrow : PrevOption();
                break;
            case ConsoleKey.Enter : SelectOption();
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
        
        for (var i = 0; i < _elements.Count; i++)
        {
            var element = _elements[i];

            if (_options.Contains(element))
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            
            if (_currentOption == element)
            {
                Console.BackgroundColor = Console.BackgroundColor = ConsoleColor.White;
            }
            
            if (_inputs.Contains(element))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                var input = element as ConsoleInputOption;
                Console.Write(input?.Text + input?.Value);
                Console.ResetColor();
                Console.WriteLine();
                    
                continue;
            }
            
            Console.WriteLine(element);
            Console.ResetColor();
        }
        
        Console.WriteLine();
        Console.SetCursorPosition(_inputPosition.X, _inputPosition.Y);
    }

    private void SetCursorPosition(Vector2 vector) => Console.SetCursorPosition(vector.X, vector.Y);
}