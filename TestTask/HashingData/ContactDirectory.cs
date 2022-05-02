namespace TestTask.HashingData;

public class ContactDirectory
{
    public IEnumerable<string> Contracts => _hashTable.Items;
    
    private TaskHashTable _hashTable = new TaskHashTable();

    public ContactDirectory()
    {
        CreateContact("Matthew Brown", "+3453334234");
        CreateContact("Semen Slepakov", "+1330004043");
        CreateContact("Vasiliy Bikov", "+9960034254");
        CreateContact("Andrew Andrew", "+222224443");
        CreateContact("Valeriy Saltikov", "+233341124");
        CreateContact("Viktor Pelevin", "+111111111");
    }

    public bool CreateContact(string name, string number)
    {
        try
        {
            _hashTable.Insert(name, number);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public string? FindContact(string name) => _hashTable.Get(name);

    public bool UpdateContact(string name, string value) => _hashTable.Update(name, value);

    public bool RemoveContact(string name) => _hashTable.Remove(name);
}