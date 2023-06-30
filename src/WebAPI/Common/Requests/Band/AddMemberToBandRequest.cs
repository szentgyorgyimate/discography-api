using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Band;

[ValidateNever]
public class AddMemberToBandRequest
{
    public int MemberId { get; set; }
}
