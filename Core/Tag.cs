namespace Core;

public enum Tag
{
    Single,
    Flat,
    House,
    SSGe,
    MyHomeGe,
    Batumi
}

public class TagDescription
{
    public static readonly Dictionary<Tag, string> Dictionary = new()
    {
        { Tag.Single, "Однушка" },
        { Tag.Batumi, "Батуми" },
        { Tag.House, "Дом" },
        { Tag.Flat, "Квартира" },
        { Tag.SSGe, "SSGe" },
        { Tag.MyHomeGe, "MyHome" },
    };
}