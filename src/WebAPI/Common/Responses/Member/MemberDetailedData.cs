using WebAPI.Common.Responses.Band;

namespace WebAPI.Common.Responses.Member;

public class MemberDetailedData : MemberData
{
    public List<BandData> Bands { get; set; }
}
