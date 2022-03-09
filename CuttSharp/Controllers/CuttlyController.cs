using CuttSharp.Models;
using CuttSharp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuttlyController : ControllerBase
    {
        private CuttlyService _cuttlyService;

        public CuttlyController(CuttlyService cuttlyService)
        {
            _cuttlyService = cuttlyService ?? throw new ArgumentNullException(nameof(cuttlyService));
        }

        // POST api/<CuttlyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            var response = await _cuttlyService.Shorten(value);
            if (response.CuttlyResponse.Url.Status == (int)CuttlyShortenCodeResponse.OK)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
