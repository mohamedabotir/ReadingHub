using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Services;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class CommunicationRepository : ICommunicationRepository
    {
        private readonly IHubContext<RealTimeCommunicationService,IHubs> _hub;
        private readonly IUserService _userService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CommunicationRepository(IHubContext<RealTimeCommunicationService, IHubs> hub, IUserService userService, IApplicationDbContext context,IMapper mapper)
        {
            _hub = hub;
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        public Task Notify(int elementId, NotificationType type )
        {
            var bookOwner = _context.Books.FirstOrDefault(e => e.Id == elementId);
            var notification = new CommunicationViewModel() { Type =type, ReceiverId = bookOwner.AuthorId, SenderId = _userService.GetUserId() };
            var addNotification = _context.Notifications.Add(_mapper.Map<CommunicationViewModel,Notification>(notification));
            _context.Complete();

            if (addNotification.Entity.Id < 0)
                GuardException.CanNotCreate(true,nameof(Notification));
           
            _hub.Clients.All.BroadcastNotification(notification);

            return Task.CompletedTask;
        }
    }
}
