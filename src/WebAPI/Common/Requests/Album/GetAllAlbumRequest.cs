namespace WebAPI.Common.Requests.Album;

public class GetAllAlbumRequest
{
    public int? BandId { get; set; }
    public int? TypeId { get; set; }
    public DateTime? ReleaseFrom { get; set; }
    public DateTime? ReleaseTo { get; set; }
}
