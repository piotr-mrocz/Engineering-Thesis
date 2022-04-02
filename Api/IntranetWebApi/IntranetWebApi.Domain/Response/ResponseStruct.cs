namespace IntranetWebApi.Models.Response
{
    public class ResponseStruct<T> : BaseResponse where T : struct
    {
        public T? Data { get; set; }

        public ResponseStruct() : base()
        {

        }

        public ResponseStruct(bool suceedded, string message, T? data) : base(suceedded, message)
        {
            Data = data;
        }
    }
}
