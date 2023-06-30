using WebAPI.Common.Responses.Member;
using WebAPI.Entities;

namespace WebAPI.Common.Mappers;

public static class MemberMappers
{
    public static MemberData ToMemberData(this Member member) =>
        new()
        {
            Id = member.Id,
            Name = member.Name
        };

    public static MemberDetailedData ToMemberDetailedData(this Member member) =>
        new()
        {
            Id = member.Id,
            Name = member.Name,
            Bands = member.Bands.Select(b => b.ToBandData()).ToList()
        };
}
