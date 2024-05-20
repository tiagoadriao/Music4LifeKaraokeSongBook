
using M4LKaraokeSongbook.Services;
using Microsoft.Extensions.Caching.Memory;
using Music4LifeKaraokeSongbookApp.Client.Services;

namespace Music4LifeKaraokeSongbookApp.Services
{
    public class Music4LifeSongbookService : IMusic4LifeSongbookService
    {
        private readonly DbHandler _dbHandler;

        public Music4LifeSongbookService(IConfiguration configuration, IMemoryCache cache)
        {
            _dbHandler = new(configuration, cache);
        }

        public async Task<Songbook?> GetSongbookAsync()
        {
            return await _dbHandler.GetSongsAsync();
        }
    }
}
