using System.Globalization;
using TestTask.Constants;
using TestTask.Extensions;
using TestTask.Figures;
using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class SquareDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    private int _sideA;

    private string _perimeter = "";
    private string _area = "";
    
    public SquareDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
    }

    public void Enter()
    {
        ClearValues();
        DrawScreen();
    }

    public void Exit()
    {
        
    }

    private void ClearValues()
    {
        _sideA = 0;
        _perimeter = "";
        _area = "";
    }

    private void DrawScreen()
    {
        _interaction.Clear()
            .AddText(FiguresConstants.EnterSquareData)
            .AddInputOption(FiguresConstants.Side, true, SideChangedListener, _sideA.ToString())
            .AddSelectionOption(FiguresConstants.Calculate, CalculateSelection)
            .IfThen(_perimeter.Length > 0 || _area.Length > 0, (interaction) =>
            {
                interaction
                    .AddText(PerimeterText())
                    .AddText(AreaText());
            })
            .AddSelectionOption(FiguresConstants.GoToFigureSelection, BackToFigureSelection)
            .AddSelectionOption(ScreensConstants.GoBack, BackToTaskSelection);
    }

    private void SideChangedListener(string text) => _sideA = text.ToIntOrZero();

    private void CalculateSelection()
    {
        if (_sideA == 0)
            return;
        
        var square = new Square(_sideA);
        _perimeter = square.Perimeter().ToString(CultureInfo.InvariantCulture);
        _area = square.Area().ToString(CultureInfo.InvariantCulture);
        
        DrawScreen();
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }

    private void BackToFigureSelection()
    {
        _stateMachine.Enter<FiguresDemonstrationState>();
    }

    private string PerimeterText() => $"{FiguresConstants.Perimeter}: {_perimeter}";
    private string AreaText() => $"{FiguresConstants.Area}: {_area}";
}