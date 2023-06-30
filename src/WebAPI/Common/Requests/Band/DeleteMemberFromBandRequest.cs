using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Band;

[ValidateNever]
public class DeleteMemberFromBandRequest
{
    public int MemberId { get; set; }
}
