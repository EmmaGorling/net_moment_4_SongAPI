using Microsoft.EntityFrameworkCore;
using SongAPI.Models;

namespace SongAPI.Data;

public class SongDbContext : DbContext {
    // Constructor
    public SongDbContext(DbContextOptions<SongDbContext> options) : base(options) {

    }

    // Create tables
    public DbSet<Song> Songs { get; set; }
    public DbSet<Category> Categories { get; set; }

    // Relation table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>()
            .HasMany(s => s.Categories)
            .WithMany(c => c.Songs)
            .UsingEntity(j => j.ToTable("SongCategories"));
    }
}