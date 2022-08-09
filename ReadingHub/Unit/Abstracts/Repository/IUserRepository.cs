using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IUserRepository
    {
        Task<bool> Register(UserViewModel model);
    }
}
