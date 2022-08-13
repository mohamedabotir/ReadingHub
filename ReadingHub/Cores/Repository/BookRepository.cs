using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IApplicationDbContext _context;
        public readonly IMapper _mapper;

        public BookRepository(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  
        }

        public async Task<int> PublishBook(BookViewModel model)
        {
            var book = new Book();  
            model.BookMimeType = model.BookFile.ContentType;
            book.BookMimeType = model.BookMimeType;

            using (var dataStream = new MemoryStream())
            {
                await model.BookFile.CopyToAsync(dataStream);
                book.BookFile = dataStream.ToArray();
            }
            book.AuthorId = model.AuthorId;
            book.BookName = model.BookName;
            book.Description = model.Description; 
            var createBook = await _context.Books.AddAsync(book);

             _context.Complete();
            if (createBook.Entity.Id < 0)
            {
                return -1;
            }
            return createBook.Entity.Id;
        }

        public Task<IEnumerable<GetBooksViewModel>> GetBooks()
        {
            var books = _context.Books.AsQueryable();

            return Task.FromResult(_mapper.Map<IEnumerable<Book>,IEnumerable<GetBooksViewModel>>(books));

        }
        public Book GetBookFile(int bookId)
        {
            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);
            

            return book;
        }
        // TO DO Get Specific Book
        public Task<GetBookViewModel> GetBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
