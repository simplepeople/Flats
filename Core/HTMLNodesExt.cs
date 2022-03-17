using HtmlAgilityPack;

namespace Core;

public static class HTMLNodesExt
{
    public static IEnumerable<HtmlNode> GetAllDescByAttr(this HtmlNode node, string name, string attrName,
        string attrValue)
    {
        return node.Descendants().Where(priceNode => priceNode.Name == name
                                                     && priceNode.Attributes != null
                                                     && priceNode.Attributes.Any(attr =>
                                                         attr.Name == attrName && attr.Value.Contains(attrValue)));
    }

    public static IEnumerable<HtmlNode> GetAllDescByClass(this HtmlNode node, string name, string attrValue)
    {
        return node.GetAllDescByAttr(name, "class", attrValue);
    }

    public static HtmlNode? GetFirstDescByClass(this HtmlNode node, string name, string attrValue)
    {
        return node.GetAllDescByClass(name, attrValue).FirstOrDefault();
    }

    public static HtmlNode? GetFirstDescByAttr(this HtmlNode node, string name, string attrName, string attrValue)
    {
        return node.GetAllDescByAttr(name, attrName, attrValue).FirstOrDefault();
    }        
}