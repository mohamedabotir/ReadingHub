using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Models
{
    public class CommentViewModel
    {
        public string Message { get; set; }
        public int BookId { get; set; }

        public CommentType CommentType { get; set; } = CommentType.bookComment;
    }
}
