namespace IntranetWebApi.Models.Response;
public class Response<T> : BaseResponse where T : class
{
    public T? Data { get; set; }

    public Response() : base()
    {

    }

    public Response(bool suceedded, string message, T? data) : base(suceedded, message)
    {
        Data = data;
    }
}
