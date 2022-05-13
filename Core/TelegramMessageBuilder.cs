using System.Text;

namespace Core;

public class TelegramMessageBuilder
{
    public List<TelegramMessage> Build(List<Advertisment> ads)
    {
        return ads.OrderBy(x => x.Id).Select(ad => new TelegramMessage
        {
            Content = $"{BuildTags(ad)} {BuildCost(ad)} {BuildLink(ad)} {BuildPublishedDate(ad)}"
        }).ToList();
    }

    private string BuildTags(Advertisment ad)
    {
        var tags = ad.Tags.Where(tag => !new[] { Tag.SSGe, Tag.MyHomeGe, Tag.Batumi }.Contains(tag)).ToArray();
        var sb = new StringBuilder();
        foreach (var tag in tags)
        {
            sb.Append("#");
            sb.Append(TagDescription.Dictionary[tag]);
            sb.Append(" ");
        }

        return sb.ToString();
    }

    private string BuildLink(Advertisment ad)
    {
        return ad.Link.ToString();
    }

    private string BuildCost(Advertisment ad)
    {
        return $"{ad.Cost}$";
    }

    private string BuildPublishedDate(Advertisment ad)
    {
        return ad.PublishedDate;
    }
}