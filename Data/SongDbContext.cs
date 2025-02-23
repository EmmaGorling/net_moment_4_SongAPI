using Microsoft.EntityFrameworkCore;
using SongAPI.Models;

namespace SongAPI.Data;

public class SongDbContext : DbContext {
    // Constructor
    public SongDbContext(DbContextOptions<SongDbContext> options) : base(options) {

    }

    // Create tables
    public DbSet<Song> Songs { get; set; }
}