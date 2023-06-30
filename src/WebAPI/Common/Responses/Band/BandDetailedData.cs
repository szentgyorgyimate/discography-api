using WebAPI.Common.Responses.Member;

namespace WebAPI.Common.Responses.Band;

public class BandDetailedData : BandData
{
    public string Genre { get; set; }
    public int FormedIn { get; set; }
    public string CountryOfOrigin { get; set; }

    public List<MemberData> Members { get; set; }
}
