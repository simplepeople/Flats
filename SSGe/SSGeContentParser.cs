using Core;
using HtmlAgilityPack;

namespace SSGe;

public class SSGeContentParser : IContentParser
{
    public async Task<List<Advertisment>> Parse(List<Content> contents)
    {
        var ads = new List<Advertisment>();
        foreach (var content in contents)
        {
            ads.AddRange(await Parse(content));
        }

        return ads;
    }

    public async Task<List<Advertisment>> Parse(Content content)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(content.HtmlText);
        var desc = doc.DocumentNode.GetAllDescByClass("div", "DesktopArticleLayout");
        var nodes = desc
            .Select(cardNode => (
                cardNode.Descendants().FirstOrDefault(descNode => descNode.Name == "a"),
                cardNode.GetFirstDescByClass("div","price-spot dalla").GetFirstDescByClass("div", "latest_price"),
                cardNode.GetFirstDescByClass("div", "add_time")
            ))
            .Select(nodesGroup =>
            {
                var urltext = nodesGroup.Item1.Attributes["href"].Value;
                var idtext = urltext.Split("--").Last();
                var ad = new Advertisment
                {
                    Id = Int64.Parse(idtext),
                    InternalId = "ssge_"+idtext,
                    Link = new Uri(@"https://ss.ge" + urltext),
                    Cost = Int32.Parse(nodesGroup.Item2.InnerText.Where(Char.IsDigit).ToArray()),
                    PublishedDate = nodesGroup.Item3.InnerText.ReplaceLineEndings().Replace(Environment.NewLine, "").Trim(),
                    Tags = content.Tags
                };
                return ad;
            }).ToList();
        return nodes;
    }
}