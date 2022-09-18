using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit;

namespace ReadingHub.Controllers
{
    public class CommentController:ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost]
        [Route("Comment")]
        public async Task<IActionResult> Comment(CommentViewModel comment) { 
        var commentResult = await unitOfWork.CommentRepository.Comment(comment);
            if (commentResult < 0)
                return BadRequest();
            await unitOfWork.CommunicationRepository.Notify(comment.BookId,NotificationType.Comment);


            return Ok(commentResult);
        }


        [Authorize]
        [HttpPost]
        [Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment([FromBody] int commentId)
        {
            var commentResult = await unitOfWork.CommentRepository.DeleteComment(commentId);

            return Ok(commentResult);
        }


        
        [HttpGet]
        [Route(nameof(GetBookComments))]
        public IActionResult GetBookComments(int bookId)
        {
            var commentResult =   unitOfWork.CommentRepository.GetBookComments(bookId);

            return Ok(commentResult);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(PostComment))]
        public async Task<IActionResult> PostComment(CommentViewModel model)
        {
            var commentResult =await unitOfWork.CommentRepository.PostComment(model);

            return Ok(commentResult);
        }

      
        [HttpGet]
        [Route(nameof(GetPostComments))]
        public  IActionResult GetPostComments(int postId)
        {
            var commentResult =  unitOfWork.CommentRepository.GetPostComments(postId);
            
            return Ok(commentResult);
        }
        [Authorize]
        [HttpDelete]
        [Route(nameof(DeletePostComments))]
        public async Task<IActionResult> DeletePostComments(int commentId)
        {
            var commentResult =await unitOfWork.CommentRepository.DeletePostComment(commentId);
            if (commentResult is false)
                return BadRequest("Can't Remove at This Time");
            
            return Ok(commentResult);
        }


    }
}
