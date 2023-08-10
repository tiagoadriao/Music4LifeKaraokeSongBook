public class Song
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SingerId { get; set; }

    public Language Language { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.Now;
}