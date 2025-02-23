using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class Category {
    // Properties
    public int Id { get; set; }
    [Required(ErrorMessage = "Ange ett namn för kategorin/genren")]
    public string? Name { get; set; }
    public List<Song> Songs { get; set; } = new();
}