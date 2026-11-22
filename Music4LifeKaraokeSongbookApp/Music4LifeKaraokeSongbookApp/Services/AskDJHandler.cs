using Music4LifeKaraokeSongbookApp.Client.Services;

namespace Music4LifeKaraokeSongbookApp.Services
{
    public class AskDJHandler : IAskDJHandler
    {
        public readonly IConfiguration _configuration;
        public readonly IHttpClientFactory _httpClientFactory;

        public AskDJHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> RequestASongAsync(AskDJRequest askDJRequest)
        {
            string url = _configuration["AskDJ:Url"]
                ?? throw new ArgumentNullException("Missing AskDJ:Url configuration");

            using HttpClient client = _httpClientFactory.CreateClient();

            //post a request with a payload in wwwform-urlencoded format to the url with the prompt as a parameter. the payload should have two string variables: a name variable and a message variable.

            var payload = new Dictionary<string, string>
            {
                { "name", askDJRequest.Name },
                { "message", askDJRequest.Message }
            };

            var content = new FormUrlEncodedContent(payload);

            HttpResponseMessage response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
