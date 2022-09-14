using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Models
{
    public class PostComment
    {
        public int Id { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}
