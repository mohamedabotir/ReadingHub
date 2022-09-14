using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class BookComment
    {
        public int Id { get; set; }

        public Book Book { get; set; }

        public int BookId { get; set; }

        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}
