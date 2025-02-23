using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class CreateSongDto {
    [Required]
    public string Artist { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public int Length { get; set; }

    // Only receive category IDs
    [Required]
    public List<int> CategoryIds { get; set; } = new List<int>();
}