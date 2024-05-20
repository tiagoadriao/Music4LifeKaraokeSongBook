using System.Net.Http.Json;

namespace Music4LifeKaraokeSongbookApp.Client.Services
{
    public class Music4LifeSongbookProxyService : IMusic4LifeSongbookService
    {
		private readonly HttpClient _client;

        public Music4LifeSongbookProxyService(HttpClient client)
        {
			_client = client;
        }

        public async Task<Songbook?> GetSongbookAsync()
        {
			try
			{
				return await _client.GetFromJsonAsync<Songbook>("/api/songbook");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return new();
			}
        }
    }
}
