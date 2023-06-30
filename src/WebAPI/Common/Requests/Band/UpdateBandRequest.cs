using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Band;

[ValidateNever]
public class UpdateBandRequest
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string CountryOfOrigin { get; set; }
    public int FormedIn { get; set; }
}
