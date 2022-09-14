using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IPostRepository
    {
        Task<int> Post(string Content);
        Task<bool> EditPost(PostViewModel model);

        Task<bool> DeletePost(int postId);


         
    }
}
