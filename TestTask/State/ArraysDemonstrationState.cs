using TestTask.Arrays;
using TestTask.UserInteraction;

namespace TestTask.State;

public class ArraysDemonstrationState : IState
{
    private const string DiagonalSumFormat = "Сумма чисел главной диагонали: {0}";
    private const string MultipleOfThreeFormat = "Сумма чисел кратных трём: {0}";
    private const string GenerateAgain = "Сгенерировать заново";
    private const string GoBack = "Назад";

    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;


    public ArraysDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
    }

    public void Enter()
    {
        DrawScreen();
    }

    public void Exit()
    {
        
    }

    private void DrawScreen()
    {
        var arr = ArraysExtension.RandomArray(10);

        _interaction.Clear()
            .AddText(arr.ToFormatString())
            .AddText(string.Format(DiagonalSumFormat, arr.DiagonalSum()))
            .AddText(string.Format(MultipleOfThreeFormat, arr.MultipleOfThreeSum()))
            .AddSelectionOption(GenerateAgain, DrawScreen)
            .AddSelectionOption(GoBack, BackToTaskSelection);
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }
}