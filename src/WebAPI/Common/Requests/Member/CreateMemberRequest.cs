using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebAPI.Common.Requests.Member;

[ValidateNever]
public class CreateMemberRequest
{
    public string Name { get; set; }
}
