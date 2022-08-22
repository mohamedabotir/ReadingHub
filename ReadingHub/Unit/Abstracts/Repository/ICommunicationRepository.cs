using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface ICommunicationRepository
    {
        Task Notify(int elementId, string type);
    }
}
