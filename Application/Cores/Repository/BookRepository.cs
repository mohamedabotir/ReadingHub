using Application.Cores.Models;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Hosting;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;


namespace ReadingHub.Cores.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IApplicationDbContext _context;
        public readonly IMapper _mapper;
        private readonly IHostEnvironment Environment;
        private readonly IUserService _userService;

        public BookRepository(IUserService userService, IHostEnvironment env, IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Environment = env;
            _userService = userService;
        }


        public async Task<int> PublishBook(BookViewModel model)
        {

            var book = _mapper.Map<BookViewModel, Book>(model);

            FileConvert(ref book, model.BookFile);

            var createBook = _context.Books.Add(book);

            _context.Complete();
            if (createBook.Entity.Id < 0)
            {
                return -1;
            }
            await SaveFileToDisk(model.Photo, createBook.Entity.Id);

            return createBook.Entity.Id;
        }
        public async Task<bool> UpdateBook(BookViewModel model)
        {

            var book = _context.Books.FirstOrDefault(x => x.Id == model.Id);
            var userId = _userService.GetUserId();
            if (book.AuthorId != userId)
                return false;
            GuardException.NotNull(book, nameof(book));
            var updatedData = _mapper.Map<BookViewModel, Book>(model);

            if (model.BookFile == null)
            {

                updatedData.BookFile = book.BookFile;
                updatedData.BookMimeType = book.BookMimeType;
            }
            else
            {
                FileConvert(ref updatedData, model.BookFile);
            }
            if (model.Photo != null)
            {
                await DeleteBookPhotoFile(book.Id);
                await SaveFileToDisk(model.Photo, book.Id);
            }


            _context.Books.Update(updatedData);
            _context.Complete();
            return true;
        }
        public async Task SaveFileToDisk(IFormFile file, int identifier)
        {
            string path = GetBookContentsDirectory();
            Directory.CreateDirectory(path);
            using (Stream fileStream = new FileStream(Path.Combine(path, identifier.ToString() + $".{file.FileName.Split('.')[1]}"), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public string GetBookContentsDirectory()
        {
            return Environment.ContentRootPath + "wwwroot/booksImgs";
        }
        static void FileConvert(ref Book book, IFormFile file)
        {
            var dataStream = new MemoryStream();

            file.CopyTo(dataStream);
            book.BookMimeType = file.ContentType;

            book.BookFile = dataStream.ToArray();
            dataStream.Dispose();

        }

        public Task<IEnumerable<GetBooksViewModel>> GetBooks(int pageId = 0)
        {
            var books = _mapper.Map<IEnumerable<Book>, IEnumerable<GetBooksViewModel>>(_context.Books.Skip(pageId * 10).Take(10)
                .AsQueryable());

            foreach (var book in books)
            {
                book.Photo = "booksImgs/" + GetBookName(book.Id);
            }
            return Task.FromResult(books);

        }
        public Book GetBookFile(int bookId)
        {
            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);


            return book;
        }
        public Task<GetBookViewModel> GetBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);

            GuardException.NotFound(book, nameof(Book));

            var convert = _mapper.Map<Book, GetBookViewModel>(book);
            convert.Photo = "booksImgs/" + GetBookName(book.Id);
            return Task.FromResult(convert);
        }

        string GetBookName(int bookId)
        {
            var files = Directory.GetFiles(GetBookContentsDirectory());

            var file = files.FirstOrDefault(x => x.Contains(bookId.ToString()));
            if (file != null)
                return bookId + "." + file.Split(".")[1];


            return null;
        }
        public async Task<bool> DeleteBook(int bookId)
        {

            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);
            GuardException.NotFound(book, nameof(Book));

            if (book.AuthorId != _userService.GetUserId())
                return false;

            _context.Books.Remove(book);
            _context.Complete();
            await DeleteBookPhotoFile(book.Id);

            return true;
        }

        public async Task DeleteBookPhotoFile(int bookId)
        {
            await Task.Run(() =>
              {
                  var filePath = Path.Combine(GetBookContentsDirectory(), GetBookName(bookId));
                  if (File.Exists(filePath))
                      File.Delete(filePath);

              });

        }
        public Task<IEnumerable<GetMyBookWithFileViewModel>> GetMyBooks()
        {
            var myBooks = _context.Books.Where(e => e.AuthorId == _userService.GetUserId()).AsEnumerable();

            var books = _mapper.Map<IEnumerable<Book>, IEnumerable<GetMyBookWithFileViewModel>>(myBooks);

            foreach (var book in books)
            {
                book.PhotoUrl = "booksImgs/" + GetBookName((int)book.Id);
            }

            return Task.FromResult(books);
        }
        public Task<int> GetCountBook()
        {
            var myBooks = _context.Books.Count();
            return Task.FromResult(myBooks);
        }

        public Task<IEnumerable<BookStatus>> GetBooksStatus()
        {
            var allBooksStatus = _context.BookStatus.Select(e => e);

            return Task.FromResult(allBooksStatus.AsEnumerable());
        }

        public Task<bool> AddOrUpdateMyBook(MyBookViewModel book)
        {
            var bookConverter = _mapper.Map<MyBookViewModel, MyBooks>(book);
            _context.MyBooks.Update(bookConverter);
            _context.Complete();
            return Task.FromResult(true);
        }

        public Task<bool> GetMyBooks(MyBookViewModel book)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMyBookStatus(int bookId)
        {
            var bookstatus = _context.MyBooks
                .Where(e => e.BookId == bookId).First();
            GuardException.NotFound(bookstatus, nameof(bookstatus));
            return Task.FromResult(bookstatus.BookStatusId);

        }

        public Task<bool> SetMyBookstatus(MyBookStatus bookStatus, string userId)
        {
            bookStatus.UserId = userId;
            _context.MyBooks
                .Update(_mapper.Map<MyBookStatus, MyBooks>(bookStatus));
            _context.Complete();
            return Task.FromResult(true);
        }
    }
}
