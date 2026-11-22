public class AskDJRequest
{
    public const int MaxNameLength = 100;
    public const int MaxMessageLength = 500;

    public string Name { get; set; } = null!;
    public string Message { get; set; } = null!;
}