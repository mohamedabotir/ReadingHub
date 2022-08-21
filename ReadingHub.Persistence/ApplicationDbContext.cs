using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Configuration;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingHub.Persistence
{
    public class ApplicationDbContext :IdentityDbContext<User> ,IApplicationDbContext
    {
        private readonly IUserService _userService;
        public DbSet<Book> Books { get ; set ; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IUserService userService):base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            
            base.OnModelCreating(builder);
        }

        public   void Complete()
        {
              this.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Track();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Track();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void Track() {
            this.ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity is Comment comment && e.State == EntityState.Added)
                {
                    comment.UserId = _userService.GetUserId();
                }

                if(e.Entity is Book book && e.State == EntityState.Added)
                {
                    book.AuthorId = _userService.GetUserId();
                }
            });
        
        }

    }
}