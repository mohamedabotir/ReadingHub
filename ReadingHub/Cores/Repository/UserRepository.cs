using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReadingHub.Cores.Models;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Cores.Repository
{
    public class UserRepository : IUserRepository
    {
        public   UserManager<User> _userManager { get; set; }
        public IMapper _mapper { get; set; }
        public UserRepository(UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
    }
}
