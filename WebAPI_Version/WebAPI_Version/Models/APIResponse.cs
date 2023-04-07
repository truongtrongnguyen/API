using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Version.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
