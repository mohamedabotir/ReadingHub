﻿using AutoMapper;
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
         

        public UnitOfWork(IApplicationDbContext context,IMapper map,UserManager<User> manager,IConfiguration config,IUserService userService, IHubContext<RealTimeCommunicationService, IHubs> hub)
        {
            BookRepository = new BookRepository(context,map);
            UserRepository = new UserRepository(context,manager, map,config);
            CommentRepository = new CommentRepository(context, map,userService);
            CommunicationRepository = new CommunicationRepository(hub,userService,context,map);
        }

        public IBookRepository BookRepository { get ; set ; }
        public IUserRepository UserRepository { get ; set ; }
        public ICommentRepository CommentRepository { get; set; }
        public ICommunicationRepository CommunicationRepository { get ; set ; }
    }
}
