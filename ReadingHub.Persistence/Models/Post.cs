using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string PostContent { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime PostTime { get; set; }



    }
}
