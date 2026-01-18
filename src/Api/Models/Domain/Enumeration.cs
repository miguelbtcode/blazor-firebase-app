namespace NetFirebase.Api.Models.Domain;

public abstract class Enumeration<TEnum>(int id, string name)
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    public int Id { get; protected init; } = id;

    public string Name { get; protected init; } = name;

    public static TEnum? FromValue(int id)
    {
        return Enumerations.TryGetValue(id, out TEnum? enumeration) ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(e => e.Name == name);
    }

    public static List<TEnum> GetValues()
    {
        return Enumerations.Values.ToList();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration<TEnum> other)
        {
            return false;
        }

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    public static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.Static
                    | System.Reflection.BindingFlags.FlattenHierarchy
            )
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}
