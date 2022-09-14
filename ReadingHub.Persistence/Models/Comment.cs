using ReadingHub.Persistence.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingHub.Persistence.Models
{
    public class Comment :IEditModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
      
        public DateTime CommentDateTime { get; set; }
        public DateTime EditDateTime { get ; set ; }

        [NotMapped]
        public CommentType CommentType;
        [NotMapped]
        public int EntityId { set; get; }



    }
    public enum CommentType { 
    bookComment,PostComment
    }
}
