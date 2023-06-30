namespace WebAPI.Entities;

public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BandId { get; set; }
    public int AlbumTypeId { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public Band Band { get; set; }
    public AlbumType AlbumType { get; set; }
}
