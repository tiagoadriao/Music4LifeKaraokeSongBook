using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
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
        using StreamReader reader = new("C:\\Users\\tiago\\source\\repos\\Music4LifeKaraokeSongBook\\Music4LifeKaraokeSongBook\\Server\\Lista Karaoke 20230801.csv", Encoding.UTF8);
        using CsvReader csvReader = new(reader, config);

        List<SongParser> songList = csvReader.GetRecords<SongParser>().ToList();

        List<IGrouping<string, SongParser>> songsGroupedBySinger = songList.GroupBy(songs => songs.Singer).ToList();

        _songbookDbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));

        _songbookDbContext.Database.EnsureCreated();

        //var i = 0;

        //songsGroupedBySinger.ForEach(singerGroup =>
        //{
        //    Singer singer = new()
        //    {
        //        Name = singerGroup.Key,
        //    };

        //    _songbookDbContext.Add(singer);
        //    i++;

        //    if (i > 100)
        //    {
        //        _songbookDbContext.SaveChanges();
        //        i = 0;
        //    }
        //});

        //_songbookDbContext.SaveChanges();

        songsGroupedBySinger.ForEach(singerGroup =>
        {
            var singerId = _songbookDbContext.Singers.First(x => x.Name == singerGroup.Key).Id;

            var i = 0;

            foreach (SongParser songParsed in singerGroup)
            {
                Song song = new()
                {
                    Name = songParsed.Song,
                    Language = songParsed.Language,
                    SingerId = singerId
                };

                _songbookDbContext.Add(song);

                i++;

                if (i > 10)
                {
                    _songbookDbContext.SaveChanges();
                    Console.WriteLine("DbContextSaved.");
                    i = 0;
                }
            }
        });

        _songbookDbContext.SaveChanges();
    }
}