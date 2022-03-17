namespace Core;

public record Advertisment
{
    public long Id { get; init; }
    
    public string InternalId { get; init; }
    public Uri Link { get; init; }
    public int Cost { get; init; }
    public string PublishedDate { get; init; }
    
    public Tag[] Tags { get; init; }
}