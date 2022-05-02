using TestTask.Extensions;
using TestTask.Recursion;
using TestTask.UserInteraction;

namespace TestTask.State;

public class RecursionDemonstrationState : IState
{
    private const string FindFibonacciDescription = "Введите номер элемента последовательности Фибоначи";
    private const string FibonacciResultFormat =
        "Элемент последовательности Фибоначи под номером {0} имеет значение {1}";
    private const string ExponentiationResultFormat = "Число {0} в степени {1} равно {2}";
    private const string ExponentiationDescription = "Введите данные для возведения в степень ниже";
    private const string Calculate = "Рассчитать";
    private const string Number = "Номер: ";
    private const string Value = "Значение: ";
    private const string Exp = "Степень: ";
    private const string GoBack = "Назад";

    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    private int _fibonacciNumber;
    private int _expValue;
    private int _exp;

    private int _fibonacciResult;
    private int _expResult;

    public RecursionDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
    }

    public void Enter()
    {
        ClearValues();
        DrawScreen();
    }

    private void ClearValues()
    {
        _fibonacciNumber = 0;
        _expValue = 0;
        _exp = 0;

        _fibonacciResult = 0;
        _expResult = 0;
    }

    public void Exit()
    {
        
    }

    private void DrawScreen()
    {
        _interaction.Clear()
            .AddText(FindFibonacciDescription)
            .AddInputOption(Number, true, OnFibonacciInputChanged, _fibonacciNumber.ToString())
            .AddSelectionOption(Calculate, CalculateFibonacci)
            .AddText("")
            .IfThen(_fibonacciResult > 0, (interaction) =>
            {
                interaction.AddText(string.Format(FibonacciResultFormat, _fibonacciNumber, _fibonacciResult));
                interaction.AddText("");
            })
            .AddText(ExponentiationDescription)
            .AddInputOption(Value, true, OnExpValueInputChanged, _expValue.ToString())
            .AddInputOption(Exp, true, OnExpInputChanged, _exp.ToString())
            .AddSelectionOption(Calculate, CalculateExp)
            .AddText("")
            .IfThen(_expResult > 0, (interaction) =>
            {
                interaction.AddText(string.Format(ExponentiationResultFormat, _expValue, _exp, _expResult));
                interaction.AddText("");
            })
            .AddSelectionOption(GoBack, BackToTaskSelection);
    }


    private void OnFibonacciInputChanged(string text) => _fibonacciNumber = text.ToIntOrZero();
    private void OnExpValueInputChanged(string text) => _expValue = text.ToIntOrZero();
    private void OnExpInputChanged(string text) => _exp = text.ToIntOrZero();

    private void CalculateFibonacci()
    {
        if (_fibonacciNumber == 0) return;

        _fibonacciResult = NumExtension.Fibonacci(_fibonacciNumber);
        DrawScreen();
    }

    private void CalculateExp()
    {

        _expResult = NumExtension.Pow(_expValue, _exp);
        DrawScreen();
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }
}