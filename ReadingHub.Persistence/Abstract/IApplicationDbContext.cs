using Microsoft.EntityFrameworkCore;
using ReadingHub.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Abstract
{
    public interface IApplicationDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<BookComment> BookComments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<MyBooks> MyBooks { get; set; }
        public void Complete();
    }
}
