
using M4LKaraokeSongbook.Services;
using Music4LifeKaraokeSongbookApp.Client.Services;

namespace Music4LifeKaraokeSongbookApp.Services
{
    public class Music4LifeSongbookService(DbHandler dbHandler) : IMusic4LifeSongbookService
    {
        private readonly DbHandler _dbHandler = dbHandler;

        public async Task<Songbook?> GetSongbookAsync()
        {
            return await _dbHandler.GetSongsAsync();
        }
    }
}
