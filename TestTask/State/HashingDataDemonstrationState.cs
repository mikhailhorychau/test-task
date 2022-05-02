using TestTask.Extensions;
using TestTask.HashingData;
using TestTask.UserInteraction;

namespace TestTask.State;

public class HashingDataDemonstrationState : IState
{
    private const string SelectOption = "Выберите требуемое действие";
    private const string Name = "ФИО: ";
    private const string Number = "Номер: ";
    private const string AddContact = "Добавить контакт";
    private const string UpdateContact = "Изменить контакт";
    private const string DeleteContact = "Удалить контакт";
    private const string FindContact = "Найти контакт";
    private const string ContactSuccessfullyAddedFormat = "Контакт {0} успешно добавлен";
    private const string ContactSuccessfullyRemoved = "Контакт {0} успешно удален";
    private const string ContactSuccessfullyUpdated = "Контакт {0} успешно обновлен";
    private const string ContactSuccessfullyFounded = "Контакт {0} найден";
    private const string ContactAlreadyExists = "Контакт {0} уже существует";
    private const string ContactNotExists = "Контакт {0} не существует";
    private const string GoBack = "Назад";

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
            .AddText(SelectOption)
            .AddText("")
            .AddInputOption(Name, false, NameChangedListener, _name)
            .AddInputOption(Number, true, NumberChangedListener, _number)
            .AddText("")
            .AddSelectionOption(AddContact, AddContactSelect)
            .AddSelectionOption(UpdateContact, UpdateContactSelect)
            .AddSelectionOption(DeleteContact, DeleteContactSelect)
            .AddSelectionOption(FindContact, FindContactSelect)
            .AddText("")
            .IfThen(_infoText.Length > 0, (interaction) =>
            {
                interaction
                    .AddText(_infoText)
                    .AddText("");
            })
            .AddSelectionOption(GoBack, BackToTaskSelection);
    }

    private void NameChangedListener(string name)
    {
        _name = name;
    }

    private void NumberChangedListener(string number)
    {
        _number = number;
    }

    private void AddContactSelect()
    {
        if (_name == "" || _number == "") return;
        
        var isCreated = _contactDirectory.CreateContact(_name, _number);
        var format = isCreated ? ContactSuccessfullyAddedFormat : ContactAlreadyExists;
        
        UpdateInfoAndRedraw(format);
    }

    private void UpdateContactSelect()
    {
        if (_name == "" || _number == "") return;
        
        var isUpdated = _contactDirectory.UpdateContact(_name, _number);
        var format = isUpdated ? ContactSuccessfullyUpdated : ContactNotExists;
        
        UpdateInfoAndRedraw(format);
    }

    private void DeleteContactSelect()
    {
        if (_name == "") return;

        var isRemoved = _contactDirectory.RemoveContact(_name);
        var format = isRemoved ? ContactSuccessfullyRemoved : ContactNotExists;
        
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

        var format = founded != null ? ContactSuccessfullyFounded : ContactNotExists;

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