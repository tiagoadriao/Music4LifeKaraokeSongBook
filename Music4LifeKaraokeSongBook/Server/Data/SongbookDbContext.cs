using Microsoft.EntityFrameworkCore;

public class SongbookDbContext : DbContext
{
    public SongbookDbContext(DbContextOptions<SongbookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Singer>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
        modelBuilder.Entity<Song>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
        //modelBuilder.Entity<Singer>().HasKey(x => new { x.Id, x.Name });
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Song> Songs { get; set; }
    public DbSet<Singer> Singers { get; set; }
}