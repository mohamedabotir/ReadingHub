using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Services;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class UserRepository : IUserRepository
    {
        public   UserManager<User> _userManager { get; set; }
        public IMapper _mapper { get; set; }
        private readonly   IConfiguration _configuration;
        public UserRepository(UserManager<User> userManager,IMapper mapper,IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }
               
        public async Task<bool> Register(UserViewModel model)
        {
           var user = await _userManager.CreateAsync(_mapper.Map<UserViewModel,User>(model),model.Password);
            if (user.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<string> Login(LoginViewModel model)
        {
           var user = await _userManager.FindByEmailAsync(model.Email);
            GuardException.NotFound(user, nameof(user));
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPassword)
               return  String.Empty;

            var token = UserService.GenerateToken(user.Id,user.UserName,user.Email,_configuration);

            return token;
        }
    }
}
