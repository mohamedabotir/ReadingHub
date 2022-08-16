using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Validations.Exceptions;
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
            var book = _mapper.Map<BookViewModel,Book>(model);

             FileConvert(ref book,model.BookFile);

             var createBook = await _context.Books.AddAsync(book);

             _context.Complete();
            if (createBook.Entity.Id < 0)
            {
                return -1;
            }
            return createBook.Entity.Id;
        }
        public Task<bool> UpdateBook(BookViewModel model)
        {
            
            var book = _context.Books.FirstOrDefault(x => x.Id == model.Id);
            GuardException.NotNull(book, nameof(book));
            var updatedData = _mapper.Map<BookViewModel, Book>(model);

            if(model.BookFile == null)
            {
                
                updatedData.BookFile = book.BookFile;
                updatedData.BookMimeType = book.BookMimeType;
            }
            else
            {
             FileConvert(ref updatedData, model.BookFile);
            }

            _context.Books.Update(updatedData);
            _context.Complete(); 
            return Task.FromResult(true);
        }

          static void FileConvert(ref Book book,IFormFile file)
        {
            var dataStream = new MemoryStream();

              file.CopyTo(dataStream);
            book.BookMimeType = file.ContentType;

            book.BookFile = dataStream.ToArray();
            

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
