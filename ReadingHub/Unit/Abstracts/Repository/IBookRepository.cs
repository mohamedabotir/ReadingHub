using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IBookRepository
    {
        Task<int> PublishBook(BookViewModel model);
    }
}
