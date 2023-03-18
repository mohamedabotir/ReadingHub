using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface ICommunicationRepository
    {
        Task Notify(int elementId, NotificationType type);
    }
}
