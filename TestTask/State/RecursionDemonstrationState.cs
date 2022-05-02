using TestTask.Constants;
using TestTask.Extensions;
using TestTask.Recursion;
using TestTask.UserInteraction;

namespace TestTask.State;

public class RecursionDemonstrationState : IState
{
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
            .AddText(RecursionConstants.FindFibonacciDescription)
            .AddInputOption(RecursionConstants.Number, true, OnFibonacciInputChanged, _fibonacciNumber.ToString())
            .AddSelectionOption(ScreensConstants.Calculate, CalculateFibonacci)
            .AddText("")
            .IfThen(_fibonacciResult > 0, (interaction) =>
            {
                interaction.AddText(string.Format(RecursionConstants.FibonacciResultFormat, _fibonacciNumber, _fibonacciResult));
                interaction.AddText("");
            })
            .AddText(RecursionConstants.ExponentiationDescription)
            .AddInputOption(RecursionConstants.Value, true, OnExpValueInputChanged, _expValue.ToString())
            .AddInputOption(RecursionConstants.Exp, true, OnExpInputChanged, _exp.ToString())
            .AddSelectionOption(ScreensConstants.Calculate, CalculateExp)
            .AddText("")
            .IfThen(_expResult > 0, (interaction) =>
            {
                interaction.AddText(string.Format(RecursionConstants.ExponentiationResultFormat, _expValue, _exp, _expResult));
                interaction.AddText("");
            })
            .AddSelectionOption(ScreensConstants.GoBack, BackToTaskSelection);
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