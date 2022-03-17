namespace Core;

public enum Tag
{
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
        { Tag.Batumi, "Батуми" },
        { Tag.House, "Дом" },
        { Tag.Flat, "Квартира" },
        { Tag.SSGe, "SSGe" },
        { Tag.MyHomeGe, "MyHome" },
    };
} 