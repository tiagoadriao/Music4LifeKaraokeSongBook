namespace Music4LifeKaraokeSongbookApp.Client.Services
{
    public interface IMusic4LifeSongbookService
    {
        public Task<Songbook?> GetSongbookAsync();
    }
}
