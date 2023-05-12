namespace Jwt_Login_API.Models
{
    public class UrlQuery
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 2;
        public string keyword { get; set; } = string.Empty;
    }
}
