using AutoMapper;
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
            book.BookMimeType = book.BookMimeType;

            using (var dataStream = new MemoryStream())
            {
                await model.BookFile.CopyToAsync(dataStream);
                book.BookFile = dataStream.ToArray();
            }
            book.AuthorId = model.AuthorId;
            book.BookName = model.BookName;
            book.Description = model.Description; 
            var createBook = await _context.Books.AddAsync(book);

            if (createBook.Entity.Id < 0)
            {
                return -1;
            }
            return createBook.Entity.Id;
        }
    }
}
