using Application.Cores.Models;
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
        [HttpPost, DisableRequestSizeLimit]
        [Route("PublishBook")]
        public async Task<IActionResult> PublishBook([FromForm] BookViewModel model)
        {

            var bookResult = await unitOfWork.BookRepository.PublishBook(model);
            if (bookResult < 0)
                return BadRequest(bookResult);
            return Ok(bookResult);
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks(int pageId = 0)
        {

            var bookResult = await unitOfWork.BookRepository.GetBooks(pageId);
            if (!bookResult.Any())
                return BadRequest(bookResult);
            return Ok(bookResult);
        }
        [HttpGet]
        [Route("GetBookFile")]
        public FileContentResult GetBookFile(int bookId)
        {
            var book = unitOfWork.BookRepository.GetBookFile(bookId);
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
        public async Task<IActionResult> UpdateBook([FromForm] BookViewModel book)
        {
            var result = await unitOfWork.BookRepository.UpdateBook(book);

            if (result is false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBook")]
        public async Task<IActionResult> GetBook(int bookId)
        {
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

        [HttpGet]
        [Route("GetBookCount")]
        public async Task<IActionResult> GetBookCount()
        {

            return Ok(await unitOfWork.BookRepository.GetCountBook());
        }


        [HttpGet]
        [Route(nameof(GetAllBooksStatus))]
        public async Task<IActionResult> GetAllBooksStatus()
        {
            return Ok(await unitOfWork.BookRepository.GetBooksStatus());
        }
        [HttpPost]
        [Route(nameof(AddOrUpdateMyBookStatus))]
        public async Task<IActionResult> AddOrUpdateMyBookStatus(MyBookViewModel mybook)
        {
            return Ok(await unitOfWork.BookRepository.AddOrUpdateMyBook(mybook));
        }


    }
}
