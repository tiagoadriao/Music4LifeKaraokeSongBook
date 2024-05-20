using Dapper;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;

namespace M4LKaraokeSongbook.Services
{
    public class DbHandler(IConfiguration configuration, IMemoryCache cache)
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMemoryCache _cache = cache;

        public async Task<Songbook?> GetSongsAsync()
        {
            var songbook = new Songbook();

                try
                {
                    songbook = await _cache.GetOrCreateAsync("Songbook", async (data) =>
                    {
                        using SqlConnection connection = new(_configuration.GetConnectionString("DefaultConnection"));

                        songbook.Songs = (await connection.QueryAsync<SongDb>(
                            @"SELECT Songs.Id, Songs.Name, Singers.Name as SingerName, Songs.LanguageId, Songs.DateAdded
                        FROM Songs
                        INNER JOIN Singers
                        ON Singers.ID = Songs.SingerID
                        ORDER BY Songs.LanguageId, SingerName, Songs.Name")).AsList();

                        return songbook;
                    });

                return songbook;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return songbook;
            }
        }
    }
}
