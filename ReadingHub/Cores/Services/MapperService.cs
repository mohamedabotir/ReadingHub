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
                .ForMember(m => m.CommentDateTime , o => o.MapFrom(o=>DateTime.Now))
                .ForMember(m=>m.EntityId,o=>o.MapFrom(e=>e.BookId));

            CreateMap<Notification, CommunicationViewModel>().ReverseMap();
        CreateMap<User, ProfileViewModel>().ReverseMap();
            CreateMap<User, EditProfileViewModel>().ReverseMap();

            CreateMap<Post, PostViewModel>().ReverseMap();

            CreateMap<BookAndPostCommentViewModel,Comment >().ReverseMap()
                .ForMember(e=>e.UserName ,o=>o.MapFrom(e=>e.User.UserName))
                .ForMember(e=>e.PhotoUrl,o=>o.MapFrom(e=>"profile/"+e.User.PhotoUrl));


            CreateMap<GetPostViewModel, Post>().ReverseMap()
                .ForMember(e => e.UserName, o => o.MapFrom(e=>e.User.UserName))
                .ForMember(e=>e.PhotoUrl,o=>o.MapFrom(e=>e.User.PhotoUrl));

            CreateMap<GetMyBookWithFileViewModel,Book >().ReverseMap()
                ;
        }
         
    }
}
