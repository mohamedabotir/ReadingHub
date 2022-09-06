using Microsoft.AspNetCore.SignalR;
using ReadingHub.Cores.Models;
using ReadingHub.Unit.Abstracts;

namespace ReadingHub.Cores.Services
{
    public class RealTimeCommunicationService :Hub<IHubs>
    {

        public async Task CreateRoom(string roomId, string userId)
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).UserConnected(userId);

        }

        

    }
}
