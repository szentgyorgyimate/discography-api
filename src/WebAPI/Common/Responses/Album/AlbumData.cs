namespace WebAPI.Common.Responses.Album;

public class AlbumData
{
    public int Id { get;set; }
    public string Name { get; set; }
    public int BandId { get; set; }
    public string BandName { get; set; }
    public int TypeId { get; set; }
    public string Type { get; set; }
    public string ReleaseDate { get; set; }
}
