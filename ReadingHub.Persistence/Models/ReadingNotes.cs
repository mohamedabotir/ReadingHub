using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class ReadingNotes
    {
        public int Id { get; set; }
        public int ReadingId { get; set; }
        public Reading Reading { get; set; }
        public DateTime DateTime { get; set; }
         public int BookPages { get; set; }

        public string Notes { get; set; }
    }
}
