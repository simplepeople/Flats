namespace Core;

public interface IAdStorage
{
    Task<List<Advertisment>> AddOrUpdate(List<Advertisment> ads);
}