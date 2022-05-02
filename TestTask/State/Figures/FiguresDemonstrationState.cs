using TestTask.Constants;
using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class FiguresDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;
    
    public FiguresDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
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
        _interaction.Clear()
            .AddText(FiguresConstants.ChooseFigureType)
            .AddText("")
            .AddSelectionOption(FiguresConstants.Rectangle, RectangleSelect)
            .AddSelectionOption(FiguresConstants.Square, SquareSelect)
            .AddSelectionOption(FiguresConstants.Rhombus, RhombusSelect)
            .AddSelectionOption(FiguresConstants.Circle, CircleSelect)
            .AddText("")
            .AddSelectionOption(ScreensConstants.GoBack, BackToTaskSelection);
    }

    private void RectangleSelect()
    {
        _stateMachine.Enter<RectangleDemonstrationState>();
    }

    private void SquareSelect()
    {
        _stateMachine.Enter<SquareDemonstrationState>();
    }

    private void RhombusSelect()
    {
        _stateMachine.Enter<RhombusDemonstrationState>();
    }

    private void CircleSelect()
    {
        _stateMachine.Enter<CircleDemonstrationState>();
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }
}