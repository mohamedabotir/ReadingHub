using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface  ICommentRepository
    {
        Task<int> Comment(CommentViewModel comment);
        Task<bool> DeleteComment(int commentId);

        Task<ProfileViewModel> GetUserInformation(string id);

        Task GetBookComments(int bookId);


    }
}
