namespace Music4LifeKaraokeSongbookApp.Client.Services
{
    public class AskDJHandler : IAskDJHandler
    {
        private readonly HttpClient _client;

        public AskDJHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> RequestASongAsync(AskDJRequest askDJRequest)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("api/requestsong", askDJRequest);
            return response.IsSuccessStatusCode;
        }
    }
}
