namespace WebAPI.Entities;

public class AlbumType
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Album> Albums { get; set; }
}
