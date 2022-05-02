using System.Globalization;
using TestTask.Constants;
using TestTask.Extensions;
using TestTask.Figures;
using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class RectangleDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    private int _sideA;
    private int _sideB;

    private string _perimeter = "";
    private string _area = "";
    
    public RectangleDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
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
        _sideB = 0;
        _perimeter = "";
        _area = "";
    }

    private void DrawScreen()
    {
        _interaction.Clear()
            .AddText(FiguresConstants.EnterRectangleData)
            .AddInputOption(FiguresConstants.Width, true, WidthChangedListener, _sideA.ToString())
            .AddInputOption(FiguresConstants.Height, true, HeightChangedListener, _sideB.ToString())
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

    private void WidthChangedListener(string text) => _sideA = text.ToIntOrZero();

    private void HeightChangedListener(string text) => _sideB = text.ToIntOrZero();

    private void CalculateSelection()
    {
        if (_sideA == 0 || _sideB == 0)
            return;
        
        var rectangle = new Rectangle(_sideA, _sideB);
        _perimeter = rectangle.Perimeter().ToString(CultureInfo.InvariantCulture);
        _area = rectangle.Area().ToString(CultureInfo.InvariantCulture);
        
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