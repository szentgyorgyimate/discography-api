using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Album;

[ValidateNever]
public class CreateAlbumRequest
{
    public string Name { get; set; }
    public int Bandid { get; set; }
    public int TypeId { get; set; }
    public DateTime ReleaseDate { get; set; }
}
