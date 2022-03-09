using System.Text.Json.Serialization;

namespace CuttSharp.Models
{
    public class ShortenResponse
    {
        public string Message { get; set; }
        public ShortenCuttlyResponse CuttlyResponse { get; set; }
    }
    public class ShortenCuttlyResponse
    {
        [JsonPropertyName("url")]
        public Url Url { get; set; }
    }

    public class Url
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("shortLink")]
        public string ShortLink { get; set; }

        [JsonPropertyName("fullLink")]
        public string FullLink { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public enum CuttlyShortenCodeResponse {
        LINK_ALREADY_SHORTENED = 1,
        IS_NOT_LINK,
        NAME_TAKEN,
        INVALID_API_KEY,
        CONTAINS_INVALID_CHARACTERS,
        BLOCKED_DOMAIN,
        OK
    }
}
