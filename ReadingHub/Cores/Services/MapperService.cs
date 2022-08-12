using AutoMapper;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Services
{
    public class MapperService:Profile
    {
        public MapperService()
        {
            CreateMap<Book,BookViewModel>().ReverseMap();
            CreateMap<Book,GetBooksViewModel>().ReverseMap();
            CreateMap<User,UserViewModel>().ReverseMap();
        }
    }
}
