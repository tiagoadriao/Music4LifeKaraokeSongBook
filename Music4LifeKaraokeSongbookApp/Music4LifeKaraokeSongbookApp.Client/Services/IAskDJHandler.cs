namespace Music4LifeKaraokeSongbookApp.Client.Services
{
    public interface IAskDJHandler
    {
        public Task<bool> RequestASongAsync(AskDJRequest askDJRequest);
    }
}
