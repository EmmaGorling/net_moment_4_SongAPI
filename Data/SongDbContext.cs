using Microsoft.EntityFrameworkCore;
using SongAPI.Models;

namespace SongAPI.Data;

public class SongDbContext : DbContext {

    public SongDbContext(DbContextOptions<SongDbContext> options) : base(options) {

    }

    public DbSet<Song> Songs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SongCategory> SongCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>()
            .HasMany(s => s.Categories)
            .WithMany(s => s.Songs)
            .UsingEntity<SongCategory>();

        // Seed data
        modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Pop"});
        modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Dance"});
        modelBuilder.Entity<Song>().HasData(new Song { Id = 1, Artist = "Lady Gaga", Title = "Poker Face", Length = 237});

        modelBuilder.Entity<SongCategory>().HasData(new SongCategory { SongId = 1, CategoryId = 1 });
        modelBuilder.Entity<SongCategory>().HasData(new SongCategory { SongId = 1, CategoryId = 2 });
    }
}