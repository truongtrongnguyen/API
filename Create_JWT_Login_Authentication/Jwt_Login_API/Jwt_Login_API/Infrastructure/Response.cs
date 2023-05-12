namespace Jwt_Login_API.Infrastructure
{
    public class Response<T>
    {
        public T @object { get; set; }
        public string Message { get; set; }
        public bool State { get; set; }
    }

    public class ResponseDefault
    {
        public string Data { get; set; }
    }

    public class Pagination<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
