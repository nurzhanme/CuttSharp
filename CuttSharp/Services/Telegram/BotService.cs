using CuttSharp.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace CuttSharp.Services.Telegram
{
    public class BotService : IBotService
    {
        private readonly TelegramConfiguration _config;

        public BotService(IOptions<TelegramConfiguration> config)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));

            Client = new TelegramBotClient(_config.AccessToken);
        }

        public TelegramBotClient Client { get; }
    }
}
