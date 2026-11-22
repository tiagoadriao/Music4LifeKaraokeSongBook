using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Music4LifeKaraokeSongbookApp.Data;

namespace M4LKaraokeSongbook.Services
{
    public class DbHandler(SongbookDbContext dbContext, IMemoryCache cache)
    {
        private readonly SongbookDbContext _dbContext = dbContext;
        private readonly IMemoryCache _cache = cache;

        public async Task<Songbook?> GetSongsAsync()
        {
            return await _cache.GetOrCreateAsync("Songbook", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

                var songs = await _dbContext.Songs
                    .Join(
                        _dbContext.Singers,
                        song => song.SingerId,
                        singer => singer.Id,
                        (song, singer) => new SongDb
                        {
                            Id = song.Id,
                            Name = song.Name,
                            SingerId = song.SingerId,
                            SingerName = singer.Name,
                            LanguageId = song.Language,
                            DateAdded = song.DateAdded
                        })
                    .OrderBy(song => song.LanguageId)
                    .ThenBy(song => song.SingerName)
                    .ThenBy(song => song.Name)
                    .ToListAsync();

                return new Songbook { Songs = songs };
            });
        }
    }
}
