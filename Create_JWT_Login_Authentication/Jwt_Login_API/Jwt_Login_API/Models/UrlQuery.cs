namespace Jwt_Login_API.Models
{
    public class UrlQuery
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string keyword { get; set; } = string.Empty;
    }
}
