public class SongDb
{
    public SongDb(int Id, string Name, int SingerId, string SingerName, Language Language)
    {
        this.Id = Id;
        this.Name = Name;
        this.SingerId = SingerId;
        this.SingerName = SingerName;
        this.Language = Language;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SingerId { get; set; }

    public string SingerName { get; set; } = null!;

    public Language Language { get; set; }
}