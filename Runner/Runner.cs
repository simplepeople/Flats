using Core;
using MyHomeGe;
using Serilog;
using SSGe;

namespace Runner;

public class Runner
{
    private readonly AdStorage _adStorage; //hack it's not concurrent
    private readonly ILogger _logger;
    private readonly TelegramMessageBuilder _messageBuilder;
    private readonly TelegramMessageSender _messageSender;
    private readonly IList<Scrapper> _scrappers;

    public Runner(TelegramMessageBuilder builder, TelegramMessageSender sender)
    {
        _logger = new LoggerConfiguration()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();
        _adStorage = new AdStorage();
        _scrappers = new[]
        {
            new Scrapper(new MyHomeGeWebContentDownloader(), new MyHomeGeContentParser()),
            new Scrapper(new SSGeWebContentDownloader(), new SSGeContentParser())
        };
        _messageBuilder = builder;
        _messageSender = sender;
    }

    /// <summary>
    /// don't run more then once
    /// </summary>
    /// <param name="pauseBetweenScrappers"></param>
    /// <param name="scrapInterval"></param>
    public async Task RunInfinite(int pauseBetweenScrappers, int scrapInterval)
    {
        while (true)
        {
            foreach (var scraper in _scrappers)
            {
                try
                {
                    var ads = await scraper.GetTopAdvertisments();
                    var newAds = await _adStorage.AddOrUpdate(ads);
                    var messages = _messageBuilder.Build(newAds);
                    await _messageSender.Send(messages);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, ex.ToString());
                }

                await Task.Delay(pauseBetweenScrappers);
            }

            await Task.Delay(scrapInterval);
        }
    }
}