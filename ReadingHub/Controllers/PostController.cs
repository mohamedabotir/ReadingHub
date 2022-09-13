using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Unit;

namespace ReadingHub.Controllers
{
    
    public class PostController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public PostController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }
        [Authorize]
        [Route(nameof(Post))]
        [HttpPost]
        public async Task<IActionResult> Post(string content) { 
        var post =await unitOfWork.PostRepository.Post(content);

            return Ok(post);
        }
    }
}
