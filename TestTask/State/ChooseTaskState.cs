using TestTask.Constants;
using TestTask.State.Figures;
using TestTask.UserInteraction;

namespace TestTask.State;

public class ChooseTaskState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    public ChooseTaskState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
    }

    public void Enter()
    {
        _interaction.Clear()
            .AddSelectionOption(ScreensConstants.Arrays, OnArraysSelect)
            .AddSelectionOption(ScreensConstants.Recursion, OnRecursionSelect)
            .AddSelectionOption(ScreensConstants.HashingData, OnHashingDataSelect)
            .AddSelectionOption(ScreensConstants.Figures, InFiguresSelect);
    }
    
    public void Exit()
    {
        
    }

    private void OnArraysSelect() => _stateMachine.Enter<ArraysDemonstrationState>();

    private void OnRecursionSelect() => _stateMachine.Enter<RecursionDemonstrationState>();

    private void OnHashingDataSelect() => _stateMachine.Enter<HashingDataDemonstrationState>();

    private void InFiguresSelect() => _stateMachine.Enter<FiguresDemonstrationState>();
    
}