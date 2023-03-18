using ReadingHub.Cores.Models;

namespace ReadingHub.Unit.Abstracts.Repository
{
    public interface  ICommentRepository
    {
        Task<int> Comment(CommentViewModel comment);

        Task<bool> DeleteComment(int commentId);

        Task<ProfileViewModel> GetUserInformation(string id);

        IEnumerable<BookAndPostCommentViewModel> GetBookComments(int bookId);

        Task<int> PostComment(CommentViewModel model);

        IEnumerable<BookAndPostCommentViewModel> GetPostComments(int postId);
        Task<bool> DeletePostComment(int postId);


    }
}
