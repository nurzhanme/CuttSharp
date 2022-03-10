using CuttSharp.Configurations;
using CuttSharp.Services;
using CuttSharp.Services.Telegram;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CuttSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ILogger<TelegramController> _logger;
        private readonly TelegramService _telegramService;
        private readonly CuttlyService _cuttlyService;
        public TelegramController(ILogger<TelegramController> logger, CuttlyService cuttlyService, TelegramService telegramService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telegramService = telegramService ?? throw new ArgumentNullException(nameof(telegramService));
            _cuttlyService = cuttlyService ?? throw new ArgumentNullException(nameof(cuttlyService));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var chatId = update.Message.Chat.Id;
            var username = update.Message.Chat.Username;

            _logger.LogInformation($"Char id {chatId} with username {username}");

            await _telegramService.SendTypingStatus(chatId);
            if (update.Type != UpdateType.Message || update.Message!.Type != MessageType.Text)
            {
                await _telegramService.SendTextMessageToSystemChat(chatId, "Please provide link");
            }

            var linkToShort = update.Message.Text;

            var res = await _cuttlyService.Shorten(linkToShort);

            await _telegramService.SendTextMessageToSystemChat(chatId, res.CuttlyResponse.Url.Status == 7 ? res.CuttlyResponse.Url.ShortLink : res.Message);

            return Ok();
        }
    }
}
