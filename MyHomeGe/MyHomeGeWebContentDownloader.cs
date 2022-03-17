using Core;

namespace MyHomeGe;

public class MyHomeGeWebContentDownloader : IContentDownloader
{
    public async Task<List<Content>> Get()
    {
        var links = new List<(Tag[] tags, string link)>
        {
            (new []{Tag.MyHomeGe, Tag.Batumi, Tag.Flat},
                "https://www.myhome.ge/ru/s/%D0%A1%D0%B4%D0%B0%D0%B5%D1%82%D1%81%D1%8F-%D0%B2-%D0%B0%D1%80%D0%B5%D0%BD%D0%B4%D1%83-%D0%BA%D0%B2%D0%B0%D1%80%D1%82%D0%B8%D1%80%D0%B0-%D0%91%D0%B0%D1%82%D1%83%D0%BC%D0%B8?Keyword=%D0%91%D0%B0%D1%82%D1%83%D0%BC%D0%B8&AdTypeID=3&PrTypeID=1&SortID=1&mapC=41.6509502%2C41.6360085&districts=776481390.776472116.776471185.777654897.776734274.776998491.776460995.776458944.776463102.776465448&cities=8742159&GID=8742159&FCurrencyID=1&FPriceFrom=200&FPriceTo=700&AreaSizeFrom=40&RoomNums=2.3.4"),
            (new []{Tag.MyHomeGe, Tag.Batumi,Tag.House},
            "https://www.myhome.ge/ru/s/%D0%A1%D0%B4%D0%B0%D0%B5%D1%82%D1%81%D1%8F-%D0%B2-%D0%B0%D1%80%D0%B5%D0%BD%D0%B4%D1%83-%D0%B4%D0%BE%D0%BC-%D0%91%D0%B0%D1%82%D1%83%D0%BC%D0%B8?Keyword=%D0%91%D0%B0%D1%82%D1%83%D0%BC%D0%B8&AdTypeID=3&PrTypeID=2&SortID=1&mapC=41.6509502%2C41.6360085&districts=776481390.776472116.776471185.777654897.776734274.776998491.776460995.776458944.776463102.776465448&cities=8742159&GID=8742159&FCurrencyID=1&FPriceFrom=300&FPriceTo=2500&AreaSizeFrom=40&RoomNums=2.3.4")
        };

        var contents = new List<Content>(links.Count);
        foreach (var link in links)
        {
            var responseString = await new HttpClient().GetStringAsync(link.link);
            await Task.Delay(5000);
            contents.Add(new Content
            {
                HtmlText = responseString,
                Tags = link.tags
            });
        }

        return contents;
    }
}