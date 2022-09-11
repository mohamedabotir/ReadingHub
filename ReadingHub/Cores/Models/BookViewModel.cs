using Microsoft.AspNetCore.Identity;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Models
{
    public class BookViewModel
    {
        public IFormFile BookFile { get; set; }

        public IFormFile Photo { get; set; }
        public int? Id { get; set; }
        public string Description { get; set; }

        public string BookName { get; set; }
        public string BookMimeType { get; set; }

     
    }
}
