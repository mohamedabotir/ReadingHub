
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ReadingHub.Persistence.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
       public ICollection<Book>Books { set; get; }
        public User()
        {
            Books = new List<Book>();
        }
    }
}
