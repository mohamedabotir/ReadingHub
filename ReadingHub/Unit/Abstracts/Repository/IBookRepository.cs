using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IBookRepository
    {
        Task<int> PublishBook(BookViewModel model);
        Task<IEnumerable<GetBooksViewModel>> GetBooks();
        Task<GetBookViewModel> GetBook(int bookId);
        Book GetBookFile(int bookId);

        Task<bool>UpdateBook(BookViewModel model);
    }
}
