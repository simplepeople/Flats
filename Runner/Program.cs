using Core;
using MyHomeGe;
using Serilog;
using SSGe;

var log = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

const int scrapInterval = 180000;
const int pauseBetweenScrappers = 5000;
var adStorage = new AdStorage();
var scrapers = new[]
{
    new Scrapper(new MyHomeGeWebContentDownloader(), new MyHomeGeContentParser()),
    new Scrapper(new SSGeWebContentDownloader(), new SSGeContentParser())
};

var messageBuilder = new TelegramMessageBuilder();
var messageSender = new TelegramMessageSender();
while (true)
{
    foreach (var scraper in scrapers)
    {
        try
        {
            var ads = await scraper.GetTopAdvertisments();
            var newAds = await adStorage.AddOrUpdate(ads);
            var messages = messageBuilder.Build(newAds);
            await messageSender.Send(messages);
        }
        catch (Exception ex)
        {
            log.Error(ex, ex.ToString());
        }

        await Task.Delay(pauseBetweenScrappers);
    }

    await Task.Delay(scrapInterval);
}