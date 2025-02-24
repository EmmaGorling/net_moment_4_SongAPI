using songAPI.Models;

namespace SongApi.Models;

public class CategoryDto {
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<SongDto>? Songs { get; set; }
}