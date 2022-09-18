using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
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

        [Authorize]
        [Route(nameof(UpdatePost))]
        [HttpPut]
        public async Task<IActionResult> UpdatePost(PostViewModel model)
        {
            var post = await unitOfWork.PostRepository.UpdatePost(model);
            if (post is false)
                return BadRequest("Can't Update At This Time");
            return Ok(post);
        }


        [Route(nameof(GetPosts))]
        [HttpGet]
        public async Task<IActionResult> GetPosts(int page=0)
        {
            var post = await unitOfWork.PostRepository.GetPosts(page);
            if (!post.Any())
                return BadRequest("No Posts Available");
            return Ok(post);
        }
        [Authorize]
        [Route(nameof(GetMyPosts))]
        [HttpGet]
        public async Task<IActionResult> GetMyPosts(int page = 0)
        {
            var post = await unitOfWork.PostRepository.GetMyPosts(page);
            if (!post.Any())
                return BadRequest("No Posts Available");
            return Ok(post);
        }


        [Authorize]
        [Route(nameof(DeletePost))]
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await unitOfWork.PostRepository.DeletePost(postId);
            if (post is false)
                return BadRequest("can't Delete at This time");
            return Ok(post);
        }


    }
}
