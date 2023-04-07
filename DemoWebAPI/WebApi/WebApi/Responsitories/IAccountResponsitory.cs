using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Responsitories
{
    public interface IAccountResponsitory
    {
        public Task<string> SignInAsync(SignInModel model);
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
    }
}
