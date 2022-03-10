using Telegram.Bot;

namespace CuttSharp.Services.Telegram
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
