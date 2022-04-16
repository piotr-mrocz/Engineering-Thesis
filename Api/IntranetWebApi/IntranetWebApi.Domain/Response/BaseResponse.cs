namespace IntranetWebApi.Models.Response;
public class BaseResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public BaseResponse()
    {
    }

    public BaseResponse(bool succeeded, string message)
    {
        Message = message;
        Succeeded = succeeded;
    }
}
