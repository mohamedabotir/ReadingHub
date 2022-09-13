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
        public IActionResult GetUserId()
        {
            return Ok(HttpContext.User.Claims.First(e => e.Type == "userId").Value);
        }

        [Route(nameof(GetUsersProfiles))]
        [HttpGet]
        public async Task<IActionResult> GetUsersProfiles() {
            var profiles = await unitOfWork.UserRepository.GetUserProfileOrUsersProfiles(Unit.Abstracts.Repository.ProfileType.Users);

            return Ok(profiles);
        }

        [HttpGet]
        [Route(nameof(CheckEmail))]
        public async Task<IActionResult> CheckEmail(string emailAddress) { 
        var isFound =await unitOfWork.UserRepository.CheckEmailAddress(emailAddress);

        return Ok(isFound);
        }
        [HttpPut]
        [Route(nameof(UpdateProfile))]
        [Authorize]
        public  IActionResult UpdateProfile([FromForm]EditProfileViewModel model) {

            unitOfWork.UserRepository.EditProfile(model);

            return Ok();
        }

    }
}
