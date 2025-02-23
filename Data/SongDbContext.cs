using Microsoft.EntityFrameworkCore;
using SongAPI.Models;

namespace SongAPI.Data;

public class SongDbContext : DbContext {

    public SongDbContext(DbContextOptions<SongDbContext> options) : base(options) {

    }

    public DbSet<Song> Songs { get; set; }
}