using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class Song {
    // Properties
    public int Id { get; set; }
    [Required]
    public string? Artist { get; set; }
    [Required]
    public string? Title  { get; set; }
    [Required]
    public int Length { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
}