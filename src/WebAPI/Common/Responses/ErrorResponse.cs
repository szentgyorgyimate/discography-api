using WebAPI.Common.Constants;

namespace WebAPI.Common.Responses;

public class ErrorResponse : BaseResponse
{
    public ErrorResponse() : base(ResponseStatuses.Error)
    {
    }

    public ErrorResponse(string message) : base(ResponseStatuses.Error)
    {
        Message = message;
    }

    public string Message { get; set; }
}
