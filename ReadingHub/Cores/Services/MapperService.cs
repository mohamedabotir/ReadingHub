using AutoMapper;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Services
{
    public class MapperService:Profile
    {
        public MapperService()
        {
            CreateMap<Book, BookViewModel>().ReverseMap()
                .ForMember(m => m.BookFile, o => o.Ignore())
                .ForMember(m => m.BookMimeType, o => o.Ignore()).ReverseMap();
            CreateMap<Book,GetBooksViewModel>().ReverseMap();

            CreateMap<Book,GetBookViewModel>().ReverseMap();

            CreateMap<User,UserViewModel>().ReverseMap();

            CreateMap<Comment, CommentViewModel>()
                .ReverseMap()
                .ForMember(m => m.CommentDateTime , o => o.MapFrom(o=>DateTime.Now));

            CreateMap<Notification, CommunicationViewModel>().ReverseMap();
        CreateMap<ProfileViewModel,User>().ReverseMap();
        }
        public Book validate(BookViewModel model)
        {
            return new Book();
        }
    }
}
