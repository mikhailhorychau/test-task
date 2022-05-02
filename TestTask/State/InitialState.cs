using TestTask.Constants;
using TestTask.UserInteraction;

namespace TestTask.State;

public class InitialState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    public InitialState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
    }

    public void Enter()
    {
        _interaction.Clear()
            .AddText(ScreensConstants.StartMessage)
            .AddText("")
            .AddSelectionOption(ScreensConstants.Ok, GoToTaskSelection);
    }

    public void Exit()
    {
        
    }

    private void GoToTaskSelection() => _stateMachine.Enter<ChooseTaskState>();
}