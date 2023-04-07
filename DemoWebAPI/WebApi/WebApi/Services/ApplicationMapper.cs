using AutoMapper;
using WebApi.Models;

namespace WebApi.Services
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
