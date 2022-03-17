namespace Core;

public class AdStorage : IAdStorage
{
    private const string filePath = "adids.txt";
    private HashSet<string>? _storage;

    private async Task<string[]> ReadOld()
    {
        try
        {
            var ids = await File.ReadAllLinesAsync(filePath);
            return ids;
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    public async Task<List<Advertisment>> AddOrUpdate(List<Advertisment> ads)
    {
        _storage ??= new HashSet<string>(await ReadOld());
        
        var newAds = ads.Where(ad => !_storage.Contains(ad.InternalId)).ToList();
        foreach (var newAd in newAds)
        {
            _storage.Add(newAd.InternalId);
        }

        await FlushNew(newAds);

        return newAds;
    }

    private async Task FlushNew(List<Advertisment> ads)
    {
        await File.AppendAllLinesAsync(filePath, ads.Select(ad => ad.InternalId.ToString()).ToArray());
    }
}