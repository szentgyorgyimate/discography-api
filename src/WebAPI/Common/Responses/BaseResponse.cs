namespace WebAPI.Common.Responses;

public abstract class BaseResponse
{
    public BaseResponse(string status)
    {
        Status = status;
    }

    public string Status { get; }
}
