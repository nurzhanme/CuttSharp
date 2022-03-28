using Cuttly;
using Cuttly.Responses.Enums;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuttlyController : ControllerBase
    {
        private Client _cuttlyClient;

        public CuttlyController(Client cuttlyClient)
        {
            _cuttlyClient = cuttlyClient ?? throw new ArgumentNullException(nameof(cuttlyClient));
        }

        // POST api/<CuttlyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            var response = await _cuttlyClient.Shorten(value);
            if (response.Url.Status == (int)ShortStatus.OK)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
