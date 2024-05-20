public class SongDb
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SingerId { get; set; }

    public string SingerName { get; set; } = null!;

    public Language LanguageId { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.Now;
}