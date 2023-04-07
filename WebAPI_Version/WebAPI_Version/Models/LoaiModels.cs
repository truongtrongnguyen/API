using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Version.Models
{
    /// <summary>
    /// Dùng làm thuộc tính để khi thêm (Create) một Loại hàng mới
    /// </summary>
    public class LoaiModels
    {
        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; }
    }
}
