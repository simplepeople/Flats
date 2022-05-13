using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Core;

public class TelegramMessageSender
{
    public TelegramMessageSender(string chatId, string token)
    {
        ChatId = chatId;
        _client = new TelegramBotClient(token);
    }

    private string ChatId { get; }
    private ITelegramBotClient _client { get; }

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