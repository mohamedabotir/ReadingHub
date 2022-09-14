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
        private readonly IHostEnvironment Environment;
        private readonly IUserService _userService;
        private readonly ISharedService _sharedService;
        public UserRepository(ISharedService service,IUserService user,IHostEnvironment env,IApplicationDbContext context,UserManager<User> userManager,IMapper mapper,IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            Environment = env;
            _userService = user;
            _sharedService = service;
        }
               
        public async Task<bool> Register(UserViewModel model)
        {
            var fileName = await SaveFileToDisk(model.Photo, model.UserName);
            model.PhotoUrl = fileName;
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
                Task.FromResult(result.AsEnumerable<ProfileViewModel>());
            }
           
                var dic = new Dictionary<string, int>();
                var books = _context.Books.AsEnumerable();
                foreach (var item in books.Select(e=>e.AuthorId))
                {
                    if (dic.ContainsKey(item)) {
                        dic[item]++;
                    }
                    else
                    {
                        dic.Add(item, 1);
                    }

                }
                foreach (var item in dic)
                {
                    if (result.Count == 3)
                        break;
                    var author = _mapper.Map<User, ProfileViewModel>(_context.Users.First(e => e.Id == item.Key));
                    author.PictureUrl = "profile/"+ author.PhotoUrl;
                    result.Add(author);
                }          
            
          



            return Task.FromResult(result.AsEnumerable<ProfileViewModel>());
        }

        

       

        public Task<bool> CheckEmailAddress(string email)
        {
            var check = _userManager.FindByEmailAsync(email);
            if(check is null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }

        public async Task EditProfile(EditProfileViewModel editProfileViewModel)
        {

            var user = _context.Users.FirstOrDefault(e => e.Id == _userService.GetUserId());
            var userBuilder = new UserBuilder(user);
            var fileName = "";
            if(editProfileViewModel.Address != null)
                userBuilder.Address(editProfileViewModel.Address);
            if (editProfileViewModel.UserName != null)
                userBuilder.Myname(editProfileViewModel.UserName);
            if(editProfileViewModel.PhoneNumber!=null)
                userBuilder.PhoneNumber(editProfileViewModel.PhoneNumber);

            if (user.Id == _userService.GetUserId() && editProfileViewModel.Photo != null)
            {

                fileName= await UpdatePicture(editProfileViewModel.Photo, user.UserName);
            }
            var builderResult = userBuilder.Build();
            builderResult.PhotoUrl = fileName;
            var result =  _context.Users.Update(builderResult);
            _context.Complete();

            
        
        }

        public async Task<string> UpdatePicture(IFormFile file, string identifier) { 
            string path = _sharedService.GetProfileContentsDirectory();
            Directory.CreateDirectory(path);
            var files = Directory.GetFiles(path);
            var fileName = "";
            if (!files.Any())
            {
                fileName= await SaveFileToDisk(file, identifier);

            }
            else

                foreach (var item in files)
                {
                    if (item.Contains(identifier))
                    {
                        File.Delete(Path.Combine(_sharedService.GetProfileContentsDirectory(), identifier + "." + item.Split(".")[1]));
                    }
                   fileName =await SaveFileToDisk(file, identifier);

                }
            return fileName;
        }

        public  Task<string> SaveFileToDisk(IFormFile file, string identifier)
        {
            string path = _sharedService.GetProfileContentsDirectory();
            Directory.CreateDirectory(path);
            var fileName = identifier + $".{file.FileName.Split('.')[1]}";
            using (Stream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                  file.CopyTo(fileStream);
            }
            return Task.FromResult(fileName);
        }

        
    }
}
