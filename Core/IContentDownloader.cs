namespace Core;

public interface IContentDownloader
{
    Task<List<Content>> Get();
}