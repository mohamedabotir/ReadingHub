using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReadingHub.Cores.Repository;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
         

        public UnitOfWork(IApplicationDbContext context,IMapper map,UserManager<User> manager,IConfiguration config,IUserService userService)
        {
            BookRepository = new BookRepository(context,map);
            UserRepository = new UserRepository(manager, map,config);
            CommentRepository = new CommentRepository(context, map,userService);
        }

        public IBookRepository BookRepository { get ; set ; }
        public IUserRepository UserRepository { get ; set ; }
        public ICommentRepository CommentRepository { get; set; }
    }
}
