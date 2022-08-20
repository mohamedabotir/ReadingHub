using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Unit;
using System.Linq;

namespace ReadingHub.Controllers
{
     
    public class UserController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var result = await unitOfWork.UserRepository.Register(model);

            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await unitOfWork.UserRepository.Login(model);

            return Ok(result);
        }
        [Route("GetUserId")]
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        public   IActionResult GetUserId()
        {
            return Ok(HttpContext.User.Claims.First(e=>e.Type=="userId").Value);
        }

    }
}
