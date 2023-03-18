using ReadingHub.Persistence.Abstract;
using ReadingHub.Unit.Abstracts.Repository;

namespace ReadingHub.Unit
{
    public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }

        public ICommunicationRepository CommunicationRepository { get; set; }

        public IPostRepository PostRepository { get; set; }

    }
}
