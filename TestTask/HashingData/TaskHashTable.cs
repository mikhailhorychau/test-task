using TestTask.HashingData.Exceptions;

namespace TestTask.HashingData;

public class TaskHashTable
{
    private class Item
    {
        public string Key { get; }
        public string Value { get; set; }

        public Item(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
    
    private const byte KeyMaxSize = 255;

    public IEnumerable<string> Items
        => _items.Values
            .SelectMany(list => list)
            .Distinct()
            .Select(item => $"{item.Key} {item.Value}")
            .ToArray();
    

    private readonly Dictionary<int, List<Item>> _items = new Dictionary<int, List<Item>>();

    public void Insert(string key, string value)
    {
        if (key.Length > KeyMaxSize)
            throw new WrongKeySizeException();

        var keyHash = GetHash(key);
        var item = new Item(key, value);

        if (_items.ContainsKey(keyHash))
        {
            var founded = FindHashItem(key, keyHash);

            if (founded != null)
            {
                throw new DuplicateKeyException();
            }
        }
        else
        {
            _items[keyHash] = new List<Item>();
        }
        
        _items[keyHash].Add(item);
    }

    public string? Get(string key) => FindHashItem(key)?.Value;

    public bool Update(string key, string newValue)
    {
        var item = FindHashItem(key);
        
        if (item != null)
            item.Value = newValue;

        return item != null;
    }

    public bool Remove(string key)
    {
        var hashKey = GetHash(key);
        var item = FindHashItem(key, hashKey);

        if (item != null)
            _items[hashKey].Remove(item);

        return item != null;
    }

    private Item? FindHashItem(string key) => FindHashItem(key, GetHash(key));
    
    private Item? FindHashItem(string key, int keyHash)
    {
        if (!_items.ContainsKey(keyHash))
            return null;

        var item = _items[keyHash].FirstOrDefault(hashItem => hashItem.Key == key);

        return item;
    }

    private static int GetHash(string key) 
        => key.Aggregate(0, (current, ch) => current + ch);
}