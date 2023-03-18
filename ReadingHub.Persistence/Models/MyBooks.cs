using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class MyBooks
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        public BookStatus BookStatus { get; set; }
        public int BookStatusId { get; set; }
    }
     
}
