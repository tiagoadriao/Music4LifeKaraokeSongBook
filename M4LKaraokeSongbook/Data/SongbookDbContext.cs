using Microsoft.EntityFrameworkCore;

public class SongbookDbContext : DbContext
{
    public SongbookDbContext(DbContextOptions<SongbookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Singer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
        });

        modelBuilder.Entity<Song>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired();
        });
    }

    public DbSet<Song> Songs { get; set; }
    public DbSet<Singer> Singers { get; set; }
}