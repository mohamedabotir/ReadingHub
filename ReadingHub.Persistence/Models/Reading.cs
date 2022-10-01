
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingHub.Persistence.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public ReadingStatus Status { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [NotMapped]
        public int BookPages { get; set; }
        [NotMapped]
        public string Notes { set; get; }

    }
   public enum ReadingStatus {
        currentReading,
        toRead,
        read
    }
}
