using TestTask.State;
using TestTask.UserInteraction;

var interaction = new ConsoleInteraction();
var stateMachine = new StateMachine(interaction);
stateMachine.Enter<InitialState>();
interaction.StartLoop();

