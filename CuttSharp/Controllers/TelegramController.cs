using Cuttly;
using Cuttly.Responses.Enums;
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
        private readonly Client _cuttlyClient;
        public TelegramController(ILogger<TelegramController> logger, Client cuttlyClient, TelegramService telegramService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telegramService = telegramService ?? throw new ArgumentNullException(nameof(telegramService));
            _cuttlyClient = cuttlyClient ?? throw new ArgumentNullException(nameof(cuttlyClient));
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

            var res = await _cuttlyClient.Shorten(linkToShort);

            await _telegramService.SendTextMessageToSystemChat(chatId, res.Url.Status == 7 ? res.Url.ShortLink : Enum.GetName(typeof(ShortStatus), res.Url.Status) ?? "Unknown error");
            return Ok();
        }
    }
}
