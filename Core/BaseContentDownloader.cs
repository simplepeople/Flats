namespace Core;

public abstract class BaseContentDownloader
{
    public async Task<List<Content>> Get(string fileName)
    {
        var path = Path.Combine(Environment.CurrentDirectory, fileName);
        string htmlText = await File.ReadAllTextAsync(path);
        return new List<Content>
        {
            new()
            {
                HtmlText = htmlText,
                Tags = Array.Empty<Tag>()
            }
        };
    }
}