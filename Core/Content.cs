namespace Core;

public record Content
{
    public string HtmlText { get; init; }
    
    public Tag[] Tags { get; init; }
}