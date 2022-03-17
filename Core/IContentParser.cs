namespace Core;

public interface IContentParser
{
    Task<List<Advertisment>> Parse(List<Content> contents);
    
    Task<List<Advertisment>> Parse(Content content);
}