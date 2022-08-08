
using Microsoft.AspNetCore.Identity;

namespace ReadingHub.Persistence.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
    }
}
