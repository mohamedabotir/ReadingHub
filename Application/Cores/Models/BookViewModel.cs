﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Cores.Models
{
    public class BookViewModel
    {
      
        public int? Id { get; set; }
        public string Description { get; set; }
        public string BookName { get; set; }
        public string BookMimeType { get; set; }
        public IFormFile BookFile { get; set; }
        public IFormFile Photo { get; set; }
        public int PageNumbers { get; set; }
    }
}
