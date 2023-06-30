using WebAPI.Common.Responses.Band;
using WebAPI.Entities;

namespace WebAPI.Common.Mappers;

public static class BandMappers
{
    public static BandData ToBandData(this Band band) =>
        new()
        {
            Id = band.Id,
            Name = band.Name
        };

    public static BandDetailedData ToBandDetailedData(this Band band) =>
        new()
        {
            Id = band.Id,
            Name = band.Name,
            Genre = band.Genre,
            FormedIn = band.FormedIn,
            CountryOfOrigin = band.CountryOfOrigin,
            Members = band.Members.Select(m => m.ToMemberData()).ToList()
        };
}
