using System.ComponentModel.DataAnnotations;

namespace WebAPI_Version.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string? PassWork { get; set; }
    }
}
