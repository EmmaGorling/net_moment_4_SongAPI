namespace songAPI.Models;

public class SongDto {
    public int Id { get; set; }
    public string? Artist { get; set; }
    public string? Title { get; set; }
    public int Length { get; set; }
}