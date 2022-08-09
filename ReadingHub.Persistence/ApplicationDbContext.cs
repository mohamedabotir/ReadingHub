using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Configuration;
using ReadingHub.Persistence.Models;

namespace ReadingHub.Persistence
{
    public class ApplicationDbContext :IdentityDbContext<User> ,IApplicationDbContext
    {
        public DbSet<Book> Books { get ; set ; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            
            base.OnModelCreating(builder);
        }

        public   void Complete()
        {
              this.SaveChanges();
        }
    }
}