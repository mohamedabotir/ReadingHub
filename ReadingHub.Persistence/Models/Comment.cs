using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime CommentDateTime { get; set; }

    }
}
