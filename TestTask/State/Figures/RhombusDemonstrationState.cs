using System.Globalization;
using TestTask.Constants;
using TestTask.Extensions;
using TestTask.Figures;
using TestTask.UserInteraction;

namespace TestTask.State.Figures;

public class RhombusDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;

    private int _sideA;
    private int _angleA;
    private int _angleB;

    private string _perimeter = "";
    private string _area = "";
    
    public RhombusDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
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
            .AddText(FiguresConstants.EnterRhombusData)
            .AddInputOption(FiguresConstants.Side, true, WidthChangedListener, _sideA.ToString())
            .AddInputOption(FiguresConstants.Angle, true, AngleAChangeListener, _angleA.ToString())
            .AddInputOption(FiguresConstants.Angle, true, AngleBChangeListener, _angleB.ToString())
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

    private void AngleAChangeListener(string text) => _angleA = text.ToIntOrZero();

    private void AngleBChangeListener(string text) => _angleB = text.ToIntOrZero();


    private void WidthChangedListener(string text) => _sideA = text.ToIntOrZero();

    private void CalculateSelection()
    {
        if (_sideA == 0 || _angleA == 0 || _angleB == 0)
            return;
        
        var rhombus = new Rhombus(_sideA, _angleA, _angleB);
        _perimeter = rhombus.Perimeter().ToString(CultureInfo.InvariantCulture);
        _area = rhombus.Area().ToString(CultureInfo.InvariantCulture);
        
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