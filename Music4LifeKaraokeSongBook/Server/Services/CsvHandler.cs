using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

public class CsvHandler
{
    private readonly SongbookDbContext _songbookDbContext;

    private readonly CsvConfiguration config;

    public CsvHandler(IConfiguration configuration, SongbookDbContext songbookDbContext)
    {
        _songbookDbContext = songbookDbContext;

        config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
            Delimiter = ";",
            HasHeaderRecord = true,
            Encoding = Encoding.UTF8,
        };
    }

    public async Task MigrateCsvToDb()
    {
        using StreamReader reader = new("C:\\Users\\Tiago.ADRIAO\\source\\repos\\tiagoadriao\\Music4LifeKaraokeSongBook\\Server\\Lista Karaoke 20230801.csv", Encoding.UTF8);
        using CsvReader csvReader = new(reader, config);

         List<SongParser> songList = csvReader.GetRecords<SongParser>().ToList();

        List<IGrouping<string, SongParser>> songsGroupedBySinger = songList.GroupBy(songs => songs.Singer).ToList();

        songsGroupedBySinger.ForEach(singerGroup =>
        {
            Singer singer = new()
            {
                Name = singerGroup.Key,
            };

            foreach (SongParser songParsed in singerGroup)
            {
                Song song = new()
                {
                    Name = songParsed.Song,
                    Language = songParsed.Language,
                    Singer = singer
                };

                singer.Songs.Add(song);
            }

            _songbookDbContext.Add(singer);

        });
        
        _songbookDbContext.SaveChanges();
    }
}