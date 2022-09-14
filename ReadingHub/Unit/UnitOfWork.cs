using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ReadingHub.Cores.Repository;
using ReadingHub.Cores.Services;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
         

        public UnitOfWork(ISharedService shared,IHostEnvironment env,IApplicationDbContext context,IMapper map,UserManager<User> manager,IConfiguration config,IUserService userService, IHubContext<RealTimeCommunicationService, IHubs> hub)
        {
            BookRepository = new BookRepository(userService,env,context,map);
            UserRepository = new UserRepository(shared,userService,env,context,manager, map,config);
            CommentRepository = new CommentRepository(shared,context, map,userService);
            CommunicationRepository = new CommunicationRepository(hub,userService,context,map);
            PostRepository = new PostRepository(userService,map,context);
        }

        public IBookRepository BookRepository { get ; set ; }
        public IUserRepository UserRepository { get ; set ; }
        public ICommentRepository CommentRepository { get; set; }
        public ICommunicationRepository CommunicationRepository { get ; set ; }
        public IPostRepository PostRepository { get ; set; }
    }
}
