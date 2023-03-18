using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface IPostRepository
    {
        Task<int> Post(string Content);
        Task<bool> EditPost(PostViewModel model);
        Task<bool> DeletePost(int postId);
        Task<bool> UpdatePost(PostViewModel model);
        Task<IEnumerable<GetPostViewModel>> GetPosts(int page=0);
        Task<IEnumerable<GetPostViewModel>> GetMyPosts(int page = 0);

    }
}
