using TestTask.State.Figures;
using TestTask.UserInteraction;

namespace TestTask.State;

public class StateMachine
{
    private readonly IUserInteraction _interaction;
    private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
    private IState? _current;

    public StateMachine(IUserInteraction interaction)
    {
        _interaction = interaction;
        _states[typeof(ChooseTaskState)] = new ChooseTaskState(this, _interaction);
        _states[typeof(ArraysDemonstrationState)] = new ArraysDemonstrationState(this, _interaction);
        _states[typeof(RecursionDemonstrationState)] = new RecursionDemonstrationState(this, _interaction);
        _states[typeof(HashingDataDemonstrationState)] = new HashingDataDemonstrationState(this, _interaction);
        _states[typeof(FiguresDemonstrationState)] = new FiguresDemonstrationState(this, _interaction);
        _states[typeof(RectangleDemonstrationState)] = new RectangleDemonstrationState(this, _interaction);
        _states[typeof(SquareDemonstrationState)] = new SquareDemonstrationState(this, _interaction);
        _states[typeof(RhombusDemonstrationState)] = new RhombusDemonstrationState(this, _interaction);
        _states[typeof(CircleDemonstrationState)] = new CircleDemonstrationState(this, _interaction);
    }
    
    public void Enter<TState>() where TState : class, IState
    {
        _current?.Exit();
        _current = GetState<TState>();
        _current?.Enter();
    }

    private TState? GetState<TState>() where TState : class, IState
    {
        return _states[typeof(TState)] as TState;
    }
}