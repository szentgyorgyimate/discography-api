using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Member;

[ValidateNever]
public class UpdateMemberRequest
{
    public string Name { get; set; }
}
