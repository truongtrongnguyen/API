namespace Jwt_Login_API.Models
{
    public class CategoryMediator
    {
        public int  Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string Descriptions { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
