using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IUserRepository
    {
        Task<bool> Register(UserViewModel model);

        Task<string> Login(LoginViewModel model);

        Task<IEnumerable<ProfileViewModel>> GetUserProfileOrUsersProfiles(ProfileType type,string id="");
        Task<bool> CheckEmailAddress(string email);
        Task EditProfile(EditProfileViewModel editProfileViewModel);
        Task<ProfileViewModel> GetMyProfile();
    }
    public enum ProfileType { 
    user,Users
    }
}
