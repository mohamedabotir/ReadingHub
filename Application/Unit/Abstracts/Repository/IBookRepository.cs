using Application.Cores.Models;
using Domain.Models;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IBookRepository
    {
        Task<int> PublishBook(BookViewModel model);
        Task<IEnumerable<GetBooksViewModel>> GetBooks(int pageId = 0);
        Task<GetBookViewModel> GetBook(int bookId);
        Book GetBookFile(int bookId);

        Task<IEnumerable<GetMyBookWithFileViewModel>> GetMyBooks();
        Task<bool> UpdateBook(BookViewModel model);
        Task<bool> DeleteBook(int bookId);

        Task<int> GetCountBook();

        Task<IEnumerable<BookStatus>> GetBooksStatus();
        Task<bool> AddOrUpdateMyBook(MyBookViewModel book);
        Task<bool> GetMyBooks(MyBookViewModel book);
        Task<bool> SetMyBookstatus(MyBookStatus bookStatus, string userId);
        Task<int> GetMyBookStatus(int bookId);
    }
}
