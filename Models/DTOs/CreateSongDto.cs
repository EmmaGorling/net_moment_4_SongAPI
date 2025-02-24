using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class CreateSongDto {
    public int? Id { get; set; }
    [Required]
    public string? Artist { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public int Length { get; set; }
    [Required]
    public List<int>? Categories { get; set; }
}