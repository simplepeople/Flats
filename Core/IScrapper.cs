namespace Core;

public interface IScrapper
{
    Task<List<Advertisment>> GetTopAdvertisments();
}