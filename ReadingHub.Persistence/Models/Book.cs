using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class Book
    {

        public int Id { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }
        public string Description { get; set; }

        public string BookName { get; set; }
        public string BookMimeType { get; set; }

        public int PageNumbers { get; set; }

        public byte[] BookFile { get; set; }

    }
}
