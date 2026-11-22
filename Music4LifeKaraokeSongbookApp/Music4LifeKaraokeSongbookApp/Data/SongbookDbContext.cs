using Microsoft.EntityFrameworkCore;

namespace Music4LifeKaraokeSongbookApp.Data;

public sealed class SongbookDbContext(DbContextOptions<SongbookDbContext> options) : DbContext(options)
{
    public DbSet<Song> Songs => Set<Song>();

    public DbSet<Singer> Singers => Set<Singer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Songs");
            entity.HasKey(song => song.Id);
            entity.Property(song => song.Language).HasColumnName("LanguageId");
        });

        modelBuilder.Entity<Singer>(entity =>
        {
            entity.ToTable("Singers");
            entity.HasKey(singer => singer.Id);
        });
    }
}
