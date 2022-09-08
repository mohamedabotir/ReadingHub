using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts
{
    public interface IHubs
    {
        Task BroadcastNotification(CommunicationViewModel notification);

        Task UserConnected(string userId);

       
        Task GetConnections(Dictionary<string, List<string>>.KeyCollection keys);
    }
}
