using Dapper;
using Microsoft.Data.Sqlite;
using Music4LifeKaraokeSongBook.Shared;

public class DbHandler
    {
        private readonly IConfiguration _configuration;
        private readonly SongbookDbContext _songbookDbContext;

    private string? SqliteConnectionString => _configuration.GetConnectionString("DefaultConnection");

    public DbHandler(IConfiguration configuration, SongbookDbContext songbookDbContext)
        {
            _configuration = configuration;
            _songbookDbContext = songbookDbContext;
        }

        public List<SongDb> GetAllSongs()
        {
        using var connection = new SqliteConnection(SqliteConnectionString);

        List<SongDb> songs = connection.Query<SongDb>(@"SELECT Songs.Id, Songs.Name, SingerId, Singers.Name as SingerName, Songs.Language
                    FROM Songs
                    LEFT JOIN Singers
                    ON Singers.Id = SingerId
                    ORDER BY Language, Singers.Name").ToList();

            return songs.OrderBy(x => x.Language).ThenBy(x => x.SingerName).ToList();
        }

        public List<SongDb> GetAllSongsByLanguage(Language language)
        {
            using var connection = new SqliteConnection(SqliteConnectionString);

            List<SongDb> songs = connection.Query<SongDb>(
                        @"SELECT Songs.Id, Songs.Name, SingerId, Singers.Name as SingerName, Songs.Language
                        FROM Songs
                        LEFT JOIN Singers
                        ON Singers.Id = SingerId
                        WHERE Language=@Language
                        ORDER BY Singers.Name",
                        new { Language = language }).ToList();

        return songs.OrderBy(x => x.Language).ThenBy(x => x.SingerName).ToList();
    }

    public List<SongDb> GetAllSongsBySinger(string singerName)
    {
        using var connection = new SqliteConnection(SqliteConnectionString);

        List<SongDb> songs = connection.Query<SongDb>(
                    @"SELECT Songs.Id, Songs.Name, SingerId, Singers.Name as SingerName, Songs.Language
                        FROM Songs
                        LEFT JOIN Singers
                        ON Singers.Id = SingerId
                        WHERE Singers.Name like '%' + @SingerName + '%'
                        ORDER BY Singers.Name",
                    new { SingerName = singerName }).ToList();

        return songs.OrderBy(x => x.Language).ThenBy(x => x.SingerName).ToList();
    }
}
