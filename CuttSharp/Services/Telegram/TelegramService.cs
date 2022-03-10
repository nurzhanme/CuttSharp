using CuttSharp.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace CuttSharp.Services.Telegram
{
    public class TelegramService
    {
        private readonly IBotService _botService;
        
        public TelegramService(IBotService botService)
        {
            _botService = botService ?? throw new ArgumentNullException(nameof(botService));
        }

        public async Task SendTextMessageToSystemChat(long chatId, string message)
        {
            await _botService.Client.SendTextMessageAsync(chatId, message);
        }


        public Task SendTypingStatus(long chatId)
        {
            return _botService.Client.SendChatActionAsync(chatId, ChatAction.Typing);
        }
    }
}
