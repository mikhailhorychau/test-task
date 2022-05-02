using TestTask.Constants;
using TestTask.Extensions;
using TestTask.HashingData;
using TestTask.UserInteraction;

namespace TestTask.State;

public class HashingDataDemonstrationState : IState
{
    private readonly StateMachine _stateMachine;
    private readonly IUserInteraction _interaction;
    private readonly ContactDirectory _contactDirectory;

    private string _infoText = "";
    private string _name = "";
    private string _number = "";

    public HashingDataDemonstrationState(StateMachine stateMachine, IUserInteraction interaction)
    {
        _stateMachine = stateMachine;
        _interaction = interaction;
        _contactDirectory = new ContactDirectory();
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
        _infoText = "";
        _name = "";
        _number = "";
    }

    private void DrawScreen()
    {
        _interaction.Clear()
            .AddText(HashingDataConstants.SelectOption)
            .AddText("")
            .AddInputOption(HashingDataConstants.Name, false, NameChangedListener, _name)
            .AddInputOption(HashingDataConstants.Number, true, NumberChangedListener, _number)
            .AddText("")
            .AddSelectionOption(ScreensConstants.Clear, ClearSelect)
            .AddText("")
            .AddSelectionOption(HashingDataConstants.AddContact, AddContactSelect)
            .AddSelectionOption(HashingDataConstants.UpdateContact, UpdateContactSelect)
            .AddSelectionOption(HashingDataConstants.DeleteContact, DeleteContactSelect)
            .AddSelectionOption(HashingDataConstants.FindContact, FindContactSelect)
            .AddText("")
            .IfThen(_infoText.Length > 0, (interaction) =>
            {
                interaction
                    .AddText(_infoText)
                    .AddText("");
            })
            .AddSelectionOption(ScreensConstants.GoBack, BackToTaskSelection);
    }

    private void NameChangedListener(string name)
    {
        _name = name;
    }

    private void NumberChangedListener(string number)
    {
        _number = number;
    }

    private void ClearSelect()
    {
        ClearValues();
        DrawScreen();
    }

    private void AddContactSelect()
    {
        if (_name == "" || _number == "") return;
        
        var isCreated = _contactDirectory.CreateContact(_name, _number);
        var format = isCreated
            ? HashingDataConstants.ContactSuccessfullyAddedFormat
            : HashingDataConstants.ContactAlreadyExists;
        
        UpdateInfoAndRedraw(format);
    }

    private void UpdateContactSelect()
    {
        if (_name == "" || _number == "") return;
        
        var isUpdated = _contactDirectory.UpdateContact(_name, _number);
        var format = isUpdated
            ? HashingDataConstants.ContactSuccessfullyUpdated
            : HashingDataConstants.ContactNotExists;
        
        UpdateInfoAndRedraw(format);
    }

    private void DeleteContactSelect()
    {
        if (_name == "") return;

        var isRemoved = _contactDirectory.RemoveContact(_name);
        var format = isRemoved
            ? HashingDataConstants.ContactSuccessfullyRemoved
            : HashingDataConstants.ContactNotExists;
        
        UpdateInfoAndRedraw(format);
    }

    private void FindContactSelect()
    {
        if (_name == "") return;

        var founded = _contactDirectory.FindContact(_name);
        if (founded != null)
        {
            _number = founded;
        }

        var format = founded != null
            ? HashingDataConstants.ContactSuccessfullyFounded
            : HashingDataConstants.ContactNotExists;

        UpdateInfoAndRedraw(format);
    }

    private void UpdateInfoAndRedraw(string format)
    {
        _infoText = string.Format(format, _name);
        DrawScreen();
    }

    private void BackToTaskSelection()
    {
        _stateMachine.Enter<ChooseTaskState>();
    }
}