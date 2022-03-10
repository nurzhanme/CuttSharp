using CuttSharp.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace CuttSharp.Services.Telegram
{
    public class TelegramService : IHostedService
    {
        private readonly ILogger<TelegramService> _logger;
        private readonly IBotService _botService;
        private readonly TelegramConfiguration _config;

        public TelegramService(ILogger<TelegramService> logger, IBotService botService, IOptions<TelegramConfiguration> config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task SendTextMessageToSystemChat(long chatId, string message)
        {
            await _botService.Client.SendTextMessageAsync(chatId, message);
        }


        public Task SendTypingStatus(long chatId)
        {
            return _botService.Client.SendChatActionAsync(chatId, ChatAction.Typing);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var baseUrl = "https://cuttsharp.azurewebsites.net";

            _logger.LogInformation($"baseUrl = {baseUrl} - yeap");

            var webhookAddress = @$"{baseUrl}/bot/{_config.AccessToken}";

            await _botService.Client.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
