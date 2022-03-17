using Core;

namespace MyHomeGe;

public class MyHomeGeFileContentDownloader : BaseContentDownloader, IContentDownloader
{
    public async Task<List<Content>> Get()
    {
        return await Get("contentexample_myhomege.html");
    }
}