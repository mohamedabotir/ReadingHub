using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Unit;

namespace ReadingHub.Controllers
{
    
    public class BookController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
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
    }
}
