using TestTask.Arrays;
using TestTask.Constants;
using TestTask.UserInteraction;

namespace TestTask.State;

public class ArraysDemonstrationState : IState
{
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
            .AddText(string.Format(ArraysConstants.DiagonalSumFormat, arr.DiagonalSum()))
            .AddText(string.Format(ArraysConstants.MultipleOfThreeFormat, arr.MultipleOfThreeSum()))
            .AddSelectionOption(ArraysConstants.GenerateAgain, DrawScreen)
            .AddSelectionOption(ScreensConstants.GoBack, BackToTaskSelection);
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }
}