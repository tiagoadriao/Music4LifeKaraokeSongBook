using Dapper;
using System.Data.SqlClient;

namespace M4LKaraokeSongbook.Services
{
    public class DbHandler(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public Songbook GetSongs()
        {
            var songbook = new Songbook();

            try
            {
                using SqlConnection connection = new(_configuration["ConnectionStrings:DefaultConnection"]);

                songbook.Songs = connection.Query<SongDb>(
                    @"SELECT Songs.Id, Songs.Name, Singers.Name as SingerName, Songs.LanguageId, Songs.DateAdded
                    FROM Songs
                    INNER JOIN Singers
                    ON Singers.ID = Songs.SingerID").AsList();
            
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
