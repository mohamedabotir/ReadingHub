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

        Task<IEnumerable<GetMyBookWithFileViewModel>> GetMyBooks();
        Task<bool>UpdateBook(BookViewModel model);
        Task<bool> DeleteBook(int bookId);

        Task<bool> MakeBookToRead(BookToReadOrReadViewModel model);
        Task<bool> MakeBookRead(BookToReadOrReadViewModel model);

    }
}
