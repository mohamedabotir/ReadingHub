using Microsoft.AspNetCore.SignalR;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Services;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class CommunicationRepository : ICommunicationRepository
    {
        private readonly IHubContext<RealTimeCommunicationService,IHubs> _hub;
        private readonly IUserService _userService;
        private readonly IApplicationDbContext _context;
        public CommunicationRepository(IHubContext<RealTimeCommunicationService, IHubs> hub, IUserService userService, IApplicationDbContext context)
        {
            _hub = hub;
            _userService = userService;
            _context = context;
        }
        public Task Notify(int elementId ,string type )
        {
            var bookOwner = _context.Books.FirstOrDefault(e => e.Id == elementId);
            var notification = new CommunicationViewModel() { Type = type, ReceiverId = bookOwner.AuthorId, SenderId = _userService.GetUserId() };

            _hub.Clients.All.BroadcastNotification(notification);

            return Task.CompletedTask;
        }
    }
}
