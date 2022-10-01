using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Models
{
    public class BookToReadOrReadViewModel
    {
        public int BookId { get; set; }
        public ReadingStatus Status { get; set; }
    }
}
