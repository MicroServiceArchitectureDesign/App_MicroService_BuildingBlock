using System.Reflection;

namespace AppMicroServiceBuildingBlock.Contract.DomainContracts.Enumerations.Base;

public abstract record BaseEnumeration<T> : IComparable<T> where T : BaseEnumeration<T>
{
    private static readonly Lazy<Dictionary<int, T>> _allItems;
    private static readonly Lazy<Dictionary<string, T>> _allItemsByName;
    private static readonly Lazy<Dictionary<string, T>> _allItemsByEnglishName;

    static BaseEnumeration()
    {
        _allItems = new Lazy<Dictionary<int, T>>(() =>
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(x => x.FieldType == typeof(T))
                .Select(x => x.GetValue(null))
                .Cast<T>()
                .ToDictionary(x => x.Value, x => x);
        });

        _allItemsByName = new Lazy<Dictionary<string, T>>(() =>
        {
            var items = new Dictionary<string, T>(_allItems.Value.Count);
            foreach (var item in _allItems.Value)
            {
                if (!items.TryAdd(item.Value.DisplayName, item.Value))
                {
                    throw new Exception(
                        $"DisplayName needs to be unique. '{item.Value.DisplayName}' already exists");
                }
            }
            return items;
        });

        _allItemsByEnglishName = new Lazy<Dictionary<string, T>>(() =>
       {
           var items = new Dictionary<string, T>(_allItems.Value.Count);
           foreach (var item in _allItems.Value)
           {
               var enDisplayName = item.Value.EnglishDisplayName ?? nameof(item);
               if (!items.TryAdd(enDisplayName, item.Value))
               {
                   throw new Exception(
                       $"EnglishDisplayName needs to be unique. '{item.Value.EnglishDisplayName}' already exists");
               }
           }
           return items;
       });
    }

    protected BaseEnumeration(int value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    protected BaseEnumeration(int value, string displayName, string englishDisplayName)
    {
        Value = value;
        DisplayName = displayName;
        EnglishDisplayName = englishDisplayName;
    }

    public int Value { get; }
    public string DisplayName { get; }
    public string? EnglishDisplayName { get; } = null;
    public override string ToString() => DisplayName;
    public string ToEnglishString() => EnglishDisplayName ?? string.Empty;

    public static IEnumerable<T> GetAll()
    {
        return _allItems.Value.Values;
    }

    public static int AbsoluteDifference(BaseEnumeration<T> firstValue, BaseEnumeration<T> secondValue)
    {
        return Math.Abs(firstValue.Value - secondValue.Value);
    }

    public static T FromValue(int value)
    {
        if (_allItems.Value.TryGetValue(value, out var matchingItem))
        {
            return matchingItem;
        }
        throw new InvalidOperationException($"'{value}' is not a valid value in {typeof(T)}");
    }

    public static string GetDisplayNameFromValue(int value)
    {
        if (_allItems.Value.TryGetValue(value, out var matchingItem))
        {
            return matchingItem.DisplayName;
        }
        throw new InvalidOperationException($"'{value}' is not a valid value in {typeof(T)}");
    }

    public static string GetEnglishDisplayNameFromValue(int value)
    {
        if (_allItems.Value.TryGetValue(value, out var matchingItem))
        {
            return matchingItem.EnglishDisplayName ?? string.Empty;
        }
        throw new InvalidOperationException($"'{value}' is not a valid value in {typeof(T)}");
    }

    public static T FromDisplayName(string displayName)
    {
        if (_allItemsByName.Value.TryGetValue(displayName, out var matchingItem))
        {
            return matchingItem;
        }
        throw new InvalidOperationException($"'{displayName}' is not a valid display name in {typeof(T)}");
    }

    public static T FromEnglishDisplayName(string englishDisplayName)
    {
        if (_allItemsByEnglishName.Value.TryGetValue(englishDisplayName, out var matchingItem))
        {
            return matchingItem;
        }
        throw new InvalidOperationException($"'{englishDisplayName}' is not a valid display name in {typeof(T)}");
    }

    public static int GetValueFromDisplayName(string displayName)
    {
        if (_allItemsByName.Value.TryGetValue(displayName, out var matchingItem))
        {
            return matchingItem.Value;
        }
        throw new InvalidOperationException($"'{displayName}' is not a valid display name in {typeof(T)}");
    }

    public static int GetValueFromEnglishDisplayName(string englishDisplayName)
    {
        if (_allItemsByEnglishName.Value.TryGetValue(englishDisplayName, out var matchingItem))
        {
            return matchingItem.Value;
        }
        throw new InvalidOperationException($"'{englishDisplayName}' is not a valid display name in {typeof(T)}");
    }

    public int CompareTo(T? other) => Value.CompareTo(other!.Value);
}