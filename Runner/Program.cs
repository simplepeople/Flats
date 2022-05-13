using Core;
using Microsoft.Extensions.Configuration;

const int scrapInterval = 180000;
const int pauseBetweenScrappers = 5000;


var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false).Build();
var messageBuilder = new TelegramMessageBuilder();
var messageSender = new TelegramMessageSender(configuration["chatId"], configuration["token"]);

var runner = new Runner.Runner(messageBuilder, messageSender);
await runner.RunInfinite(pauseBetweenScrappers, scrapInterval);