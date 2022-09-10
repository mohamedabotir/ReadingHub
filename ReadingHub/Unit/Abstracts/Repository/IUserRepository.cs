using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IUserRepository
    {
        Task<bool> Register(UserViewModel model);

        Task<string> Login(LoginViewModel model);

        Task<IEnumerable<ProfileViewModel>> GetUserProfileOrUsersProfiles(ProfileType type,string id="");
    }
    public enum ProfileType { 
    user,Users
    }
}
