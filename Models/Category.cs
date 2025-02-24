using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SongAPI.Models;

public class Category {
    // Properties
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [JsonIgnore]
    public List<Song>? Songs { get; set; }
}