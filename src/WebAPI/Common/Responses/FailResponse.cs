using System.Text.Json.Serialization;
using WebAPI.Common.Constants;

namespace WebAPI.Common.Responses;

public class FailResponse : BaseResponse
{
    public FailResponse(string message) : base(ResponseStatuses.Fail)
    {
        Message = message;
    }

    public string Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string[]> Data { get; set; }
}
