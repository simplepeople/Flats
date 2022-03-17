using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Core;

public class TelegramMessageSender
{
    private const string Token = "5260004360:AAFVZz6emp8QiYfrBcuHgvBpuj5IHQ6a7cA";
    private const string ChatId = "-1001568019930";
    private readonly ITelegramBotClient _client = new TelegramBotClient(Token);

    public async Task Send(TelegramMessage message)
    {
        await _client.SendTextMessageAsync(ChatId, message.Content, ParseMode.Html);
    }
    
    public async Task Send(IEnumerable<TelegramMessage> messages)
    {
        foreach (var message in messages)
        {
            await Send(message);
            await Task.Delay(5000);
        }
    }
}