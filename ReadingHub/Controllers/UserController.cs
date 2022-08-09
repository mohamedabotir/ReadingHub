using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingHub.Cores.Models;
using ReadingHub.Unit;

namespace ReadingHub.Controllers
{
     
    public class UserController : ApiController
    {
        private IUnitOfWork unitOfWork;

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
    }
}
