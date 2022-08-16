﻿using AutoMapper;
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
            CreateMap<User,UserViewModel>().ReverseMap();
        }
        public Book validate(BookViewModel model)
        {
            return new Book();
        }
    }
}
