using System.Globalization;
using Core;
using HtmlAgilityPack;

namespace MyHomeGe;

public class MyHomeGeContentParser : IContentParser
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
        var nodes = doc.DocumentNode.GetAllDescByClass("div", "statement-card")
            .Select(statementCardNode => (statementCardNode,
                statementCardNode.GetFirstDescByClass("a", "card-container")))
            .Where(pair => pair.Item2 != null)
            .Select(pair => (pair.statementCardNode, pair.Item2,
                    pair.Item2.Descendants().FirstOrDefault(priceNode =>
                        priceNode.Name == "b" && priceNode.Attributes != null && priceNode.Attributes.Any(attr =>
                            attr.Name == "class" && attr.Value.Contains("item-price-usd"))),
                    pair.Item2.Descendants().FirstOrDefault(priceNode =>
                        priceNode.Name == "div" && priceNode.Attributes != null && priceNode.Attributes.Any(attr =>
                            attr.Name == "class" && attr.Value.Contains("statement-date")))
                ))
            .Select(nodesGroup =>
            {
                var id = Int64.Parse(nodesGroup.statementCardNode.Attributes["data-product-id"].Value);
                return new Advertisment
                {
                    Id = id,
                    InternalId = $"mygomege_{id}",
                    Tags = content.Tags,
                    Link = new Uri(nodesGroup.Item2.Attributes["href"].Value),
                    Cost = (int)Decimal.Parse(nodesGroup.Item3.InnerText, NumberStyles.Currency),
                    PublishedDate = nodesGroup.Item4.InnerText
                };
            }).ToList();
        return nodes;
    }
}