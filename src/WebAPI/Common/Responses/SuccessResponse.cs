using WebAPI.Common.Constants;

namespace WebAPI.Common.Responses;

public class SuccessResponse : BaseResponse
{
    public SuccessResponse() : base(ResponseStatuses.Success)
    {
    }
}
