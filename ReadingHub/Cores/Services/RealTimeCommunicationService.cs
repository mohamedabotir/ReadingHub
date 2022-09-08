using Microsoft.AspNetCore.SignalR;
using ReadingHub.Cores.Models;
using ReadingHub.Unit.Abstracts;

namespace ReadingHub.Cores.Services
{
    public class RealTimeCommunicationService :Hub<IHubs>
    {
        public  static Dictionary<string,List<string>> connections = new Dictionary<string,List<string>>();
        public RealTimeCommunicationService()
        {

        }
        public async Task CreateRoom(string roomId, string userId)
        {
            connections[roomId] = new List<string>();
            await this.Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).UserConnected(userId);

        }

        public  Task Connect(string roomId, string userId) {
            connections[roomId].Add(userId);
            return  Clients.Group(roomId).UserConnected(userId);

        }

        public async Task GetRooms() {
            await Clients.All.GetConnections(connections.Keys);
        }

        public async Task Chat(string roomId, string userId,string message) {
            if (connections.ContainsKey(roomId)) {
            await Clients.Group(roomId).UserConnected(message);
            }
        
        }




    }
}
