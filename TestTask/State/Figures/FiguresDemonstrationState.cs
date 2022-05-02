using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class FiguresDemonstrationState : IState
{
    private const string ChooseFigureType = "Ввыберите тип фигуры";
    private const string Rectangle = "Прямоугольник";
    private const string Square = "Квадрат";
    private const string Rhombus = "Ромб";
    private const string Circle = "Круг";
    private const string GoBack = "Назад";

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
            .AddText(ChooseFigureType)
            .AddText("")
            .AddSelectionOption(Rectangle, RectangleSelect)
            .AddSelectionOption(Square, SquareSelect)
            .AddSelectionOption(Rhombus, RhombusSelect)
            .AddSelectionOption(Circle, CircleSelect)
            .AddText("")
            .AddSelectionOption(GoBack, BackToTaskSelection);
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