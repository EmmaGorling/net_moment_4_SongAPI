using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class CreateSongDto {
    [Required(ErrorMessage = "Ange en artist")]
    public string Artist { get; set; }
    
    [Required(ErrorMessage = "Ange en låttitel")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Ange låtens längd i sekunder")]
    public int Length { get; set; }

    // Only receive category IDs
    [Required(ErrorMessage = "Ange en eller flera kategorier")]
    public List<int> CategoryIds { get; set; } = new List<int>();
}