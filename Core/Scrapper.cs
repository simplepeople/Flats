namespace Core;

public class Scrapper : IScrapper
{
    private readonly IContentDownloader _contentDownloader;
    private readonly IContentParser _contentParser;

    public Scrapper(IContentDownloader contentDownloader, IContentParser contentParser)
    {
        _contentDownloader = contentDownloader;
        _contentParser = contentParser;
    }

    public async Task<List<Advertisment>> GetTopAdvertisments()
    {
        var content = await _contentDownloader.Get();
        var ads = await _contentParser.Parse(content);
        return ads;
    }
}