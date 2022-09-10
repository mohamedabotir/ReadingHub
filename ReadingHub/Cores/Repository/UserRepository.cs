using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReadingHub.Cores.Models;
using ReadingHub.Cores.Services;
using ReadingHub.Cores.Validations.Exceptions;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using ReadingHub.Unit.Abstracts.Repository;
using System.Collections;

namespace ReadingHub.Cores.Repository
{
    public class UserRepository : IUserRepository
    {
        public   UserManager<User> _userManager { get; set; }
        public IMapper _mapper { get; set; }
        private readonly   IConfiguration _configuration;
        private readonly IApplicationDbContext _context;
        public UserRepository(IApplicationDbContext context,UserManager<User> userManager,IMapper mapper,IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
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

        public Task<IEnumerable<ProfileViewModel>> GetUserProfileOrUsersProfiles(ProfileType type,string id="")
        {
            var result = new List<ProfileViewModel>();

            if(type== ProfileType.user)
            {
                result = (List<ProfileViewModel>)_mapper.Map<IEnumerable<User>, IEnumerable<ProfileViewModel>>(_context.Users.Where(e => e.Id == id).AsEnumerable());
            }
            else
            {
                var dic = new Dictionary<string, int>();
                var books = _context.Books.AsEnumerable();
                foreach (var item in books)
                {
                    if (dic.ContainsKey(item.AuthorId)) {
                        dic[item.AuthorId]++;
                    }
                    else
                    {
                        dic.Add(item.AuthorId, 1);
                    }

                }
                foreach (var item in dic)
                {
                    if (result.Count == 3)
                        break;
                    result.Add(_mapper.Map<User, ProfileViewModel>(_context.Users.First(e=>e.Id==item.Key)));
                }          
            }



            return Task.FromResult(result.AsEnumerable<ProfileViewModel>());
        }
    }
}
