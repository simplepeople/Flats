using Core;

namespace SSGe;

public class SSGeFileContentDownloader : BaseContentDownloader, IContentDownloader
{
    public async Task<List<Content>> Get()
    {
        return await Get("contentexample_ssge.html");
    }
}