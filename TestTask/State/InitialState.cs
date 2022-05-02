using TestTask.UserInteraction;

namespace TestTask.State;

public class InitialState : IState
{
    private const string StartMessage = "Управление осуществляется с помощью стрелок и клавиши Enter.\n" +
                                        "Белым цветом подсвечивается текущий интерактивный элемент. \n" +
                                        "Синий шрифт - активные кнопки. \n" +
                                        "Зеленый шрифт - поля ввода значений ";

    private const string Ok = "Понятно";

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
            .AddText(StartMessage)
            .AddText("")
            .AddSelectionOption(Ok, GoToTaskSelection);
    }

    public void Exit()
    {
        
    }

    private void GoToTaskSelection() => _stateMachine.Enter<ChooseTaskState>();
}