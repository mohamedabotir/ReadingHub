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
         

        public UnitOfWork(IHostEnvironment env,IApplicationDbContext context,IMapper map,UserManager<User> manager,IConfiguration config,IUserService userService, IHubContext<RealTimeCommunicationService, IHubs> hub)
        {
            BookRepository = new BookRepository(userService,env,context,map);
            UserRepository = new UserRepository(userService,env,context,manager, map,config);
            CommentRepository = new CommentRepository(context, map,userService);
            CommunicationRepository = new CommunicationRepository(hub,userService,context,map);
        }

        public IBookRepository BookRepository { get ; set ; }
        public IUserRepository UserRepository { get ; set ; }
        public ICommentRepository CommentRepository { get; set; }
        public ICommunicationRepository CommunicationRepository { get ; set ; }
    }
}
