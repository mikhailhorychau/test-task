using System.Globalization;
using TestTask.Constants;
using TestTask.Extensions;
using TestTask.Figures;
using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class CircleDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    private int _radius;

    private string _perimeter = "";
    private string _area = "";
    
    public CircleDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
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
        _radius = 0;
        _perimeter = "";
        _area = "";
    }

    private void DrawScreen()
    {
        _interaction.Clear()
            .AddText(FiguresConstants.EnterRectangleData)
            .AddInputOption(FiguresConstants.Radius, true, RadiusChangedListener, _radius.ToString())
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

    private void RadiusChangedListener(string text) => _radius = text.ToIntOrZero();

    private void CalculateSelection()
    {
        if (_radius == 0)
            return;
        
        var circle = new Circle(_radius);
        _perimeter = circle.Perimeter().ToString(CultureInfo.InvariantCulture);
        _area = circle.Area().ToString(CultureInfo.InvariantCulture);
        
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