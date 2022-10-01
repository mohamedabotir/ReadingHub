using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Unit;

namespace ReadingHub.Controllers
{
    
    public class BookController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        
        [Authorize]
        [HttpPost,DisableRequestSizeLimit]
        [Route("PublishBook")]
       public async Task<IActionResult> PublishBook([FromForm]BookViewModel model) {
              
             var bookResult = await unitOfWork.BookRepository.PublishBook(model);
              if(bookResult <0)
                return BadRequest(bookResult);
            return Ok(bookResult);
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public   async Task< IActionResult> GetAllBooks()
        {

            var bookResult = await  unitOfWork.BookRepository.GetBooks();
            if (!bookResult.Any())
                return BadRequest(bookResult);
            return Ok(bookResult);
        }
        [HttpGet]
        [Route("GetBookFile")]
        public   FileContentResult GetBookFile(int bookId)
        {
            var book =   unitOfWork.BookRepository.GetBookFile(bookId);
            if (book != null)
            {
                if (book.BookFile.Length == 0)
                    return null;
                return File(book.BookFile, book.BookMimeType);

            }
            else
                return null;
        }


        [HttpPut]
        [Route("UpdateBook")]
        [Authorize]
        public async Task<IActionResult> UpdateBook([FromForm] BookViewModel book) {
            var result =await unitOfWork.BookRepository.UpdateBook(book);
        
             if(result is false)
                return BadRequest(result);
             return Ok(result);
        }

        [HttpGet]
        [Route("GetBook")]
        public async Task<IActionResult> GetBook(int bookId) {
            var book = await unitOfWork.BookRepository.GetBook(bookId);
            return Ok(book);
        
        }

        [HttpGet]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var book = await unitOfWork.BookRepository.DeleteBook(bookId);
            return Ok(book);

        }

        [Authorize]
        [HttpGet]
        [Route("GetMyBooks")]
        public async Task<IActionResult> GetMyBooks()
        {
            var book = await unitOfWork.BookRepository.GetMyBooks();
            return Ok(book);

        }

        [Authorize]
        [HttpPost]
        [Route("MarkAsWantToRead")]
        public async Task<IActionResult> MarkAsWantToRead(BookToReadOrReadViewModel model)
        {
            var book = await unitOfWork.BookRepository.MakeBookToRead(model);
            if (!book)
                return BadRequest(book);
            return Ok(book);

        }


    }
}
