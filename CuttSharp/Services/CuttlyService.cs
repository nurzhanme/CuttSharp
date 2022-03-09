using CuttSharp.Configurations;
using CuttSharp.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CuttSharp.Services
{
    public class CuttlyService
    {
        private readonly CuttlyConfiguration _cuttlyConfiguration;
        private readonly HttpClient _httpClient;
        
        public CuttlyService(IOptions<CuttlyConfiguration> configuration, HttpClient httpClient)
        {
            _cuttlyConfiguration = configuration.Value ?? throw new ArgumentNullException(nameof(configuration));

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = new Uri("https://cutt.ly/api/api.php");

            _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ShortenResponse> Shorten(string urlToShorten)
        {
            string urlParameters = $"?key={_cuttlyConfiguration.ApiKey}&short={urlToShorten}";

            var responseMessage = await _httpClient.GetAsync(urlParameters);

            var cuttlyResponse = JsonSerializer.Deserialize<ShortenCuttlyResponse>(await responseMessage.Content.ReadAsStringAsync()) ?? new ShortenCuttlyResponse();

            var message = Enum.GetName(typeof(CuttlyShortenCodeResponse), cuttlyResponse.Url.Status) ?? "Unknown error";

            return new() { CuttlyResponse = cuttlyResponse, Message = message};

        }
    }
}
