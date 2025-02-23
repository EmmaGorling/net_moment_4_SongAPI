using System.ComponentModel.DataAnnotations;

namespace SongAPI.Models;

public class Song {
    public int Id { get; set; }
    [Required(ErrorMessage = "Ange en artist")]
    public string? Artist { get; set; }
    [Required(ErrorMessage = "Ange en låttitel")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Ange låtens längd i sekunder")]
    public int Length { get; set; }

    public string? Category { get; set; }
}