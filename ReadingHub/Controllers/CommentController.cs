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
           await unitOfWork.CommunicationRepository.Notify(comment.BookId,NotificationType.Comment);


            if (commentResult < 0)
                return BadRequest();
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


    }
}
