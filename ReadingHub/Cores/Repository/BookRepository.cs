using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public BookRepository(IUserService userService,IHostEnvironment env,IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  
            Environment = env;
            _userService = userService;
        }
       

        public async Task<int> PublishBook(BookViewModel model)
        {

            var book = _mapper.Map<BookViewModel,Book>(model);

             FileConvert(ref book,model.BookFile);

             var createBook =  _context.Books.Add(book);

             _context.Complete();
            if (createBook.Entity.Id < 0)
            {
                return -1;
            }
           await SaveFileToDisk(model.Photo,createBook.Entity.Id);

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

            if(model.BookFile == null)
            {
                
                updatedData.BookFile = book.BookFile;
                updatedData.BookMimeType = book.BookMimeType;
            } else
            {
             FileConvert(ref updatedData, model.BookFile);
            }
            if(model.Photo != null)
            {
              await DeleteBookPhotoFile(book.Id);
              await  SaveFileToDisk(model.Photo, book.Id);
            }
           

            _context.Books.Update(updatedData);
            _context.Complete(); 
            return true;
        }
        public  async Task SaveFileToDisk(IFormFile file,int identifier) {
            string path = GetBookContentsDirectory();
            Directory.CreateDirectory(path);
            using (Stream fileStream = new FileStream(Path.Combine(path,identifier.ToString()+$".{file.FileName.Split('.')[1]}"), FileMode.Create))
            {
               await file.CopyToAsync(fileStream);
            }
        }

        public string GetBookContentsDirectory() {
            return Environment.ContentRootPath + "wwwroot/booksImgs";
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
        public Task<GetBookViewModel> GetBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);

            GuardException.NotFound(book, nameof(Book));

            var convert = _mapper.Map<Book, GetBookViewModel>(book);
            convert.Photo =  "booksImgs/" + GetBookName(book.Id);
            return Task.FromResult(convert);
        }

        string GetBookName(int bookId) {
           var files =  Directory.GetFiles(GetBookContentsDirectory());

            var file = files.FirstOrDefault(x => x.Contains(bookId.ToString()));
            if(file!=null)
                return bookId + "." + file.Split(".")[1];


            return null;
        }
        public async Task<bool> DeleteBook(int bookId)
        {
 
            var book = _context.Books.FirstOrDefault(e => e.Id == bookId);
            GuardException.NotFound(book, nameof(Book));

            if(book.AuthorId != _userService.GetUserId())
                return false;

            _context.Books.Remove(book);
            _context.Complete();
            await DeleteBookPhotoFile(book.Id);

            return true;
        }

        public async Task DeleteBookPhotoFile(int bookId) {
          await  Task.Run(() =>
            {
                var filePath = Path.Combine(GetBookContentsDirectory(), GetBookName(bookId));
                if(File.Exists(filePath))
                File.Delete(filePath);

            }); 

        }
        public Task<IEnumerable<GetMyBookWithFileViewModel>> GetMyBooks()
        {
            var myBooks = _context.Books.Where(e => e.AuthorId == _userService.GetUserId()).AsEnumerable();

            var books = _mapper.Map<IEnumerable<Book>, IEnumerable<GetMyBookWithFileViewModel>>(myBooks);

            foreach (var book in books)
            {
                book.PhotoUrl = "booksImgs/"+GetBookName((int)book.Id);
            }

            return Task.FromResult(books);       
        }

        public Task<bool> MakeBookToRead(BookToReadOrReadViewModel model)
        {
            var bookCheck = _context.Books.FirstOrDefault(e => e.Id == model.BookId);
            if (bookCheck is null)
                return Task.FromResult(false);
            var readingCheck = _context.Readings
                .FirstOrDefault(e => e.BookId == model.BookId && e.UserId == _userService.GetUserId());
            if (readingCheck is not null)
                return Task.FromResult(false);
            model.Status = ReadingStatus.toRead; 
            var book = _mapper.Map<BookToReadOrReadViewModel, Reading>(model);
            var result= _context.Readings.Add(book);
            _context.Complete();
            if(result.Entity.Id<0)
               return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
