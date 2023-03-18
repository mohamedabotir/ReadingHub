namespace ReadingHub.Cores.Models
{
    public class BookAndPostCommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CommentDateTime { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string PhotoUrl { get; set; }
    }
}
