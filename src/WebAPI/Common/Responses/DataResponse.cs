namespace WebAPI.Common.Responses;

public class DataResponse<T> : SuccessResponse where T : new()
{
    public T Data { get; set; }

    public DataResponse() 
    {
        Data = new T();
    }

    public DataResponse(T data)
    {
        Data = data;
    }
}
