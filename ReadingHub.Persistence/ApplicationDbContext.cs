using Domain.Configuration;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Configuration;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit.Abstracts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        private readonly IUserService _userService;
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<BookComment> BookComments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<MyBooks> MyBooks { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new BookStatusConfiguration());
            builder.ApplyConfiguration(new MyBooksConfiguration());
            base.OnModelCreating(builder);
        }

        public void Complete()
        {
            Track();
            this.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        private void Track()
        {
            this.ChangeTracker.Entries().ToList().ForEach(e =>
            {
                if (e.Entity is Comment comment && e.State == EntityState.Added)
                {
                    comment.UserId = _userService.GetUserId();
                    this.SaveChanges();
                    if (comment.CommentType == CommentType.bookComment)
                    {
                        this.BookComments.Add(new BookComment { CommentId = comment.Id, BookId = comment.EntityId });
                        this.SaveChanges();
                    }
                    else if (comment.CommentType == CommentType.PostComment)
                    {
                        this.PostComments.Add(new PostComment { CommentId = comment.Id, PostId = comment.EntityId });
                        this.SaveChanges();
                    }

                }

                if (e.Entity is Book book && (e.State == EntityState.Added || e.State == EntityState.Modified))
                {
                    book.AuthorId = _userService.GetUserId();
                }

                if (e.Entity is Post post && e.State == EntityState.Added)
                {

                    post.UserId = _userService.GetUserId();
                    post.PostTime = DateTime.Now;
                }
                if (e.Entity is IEditModel entity && e.State == EntityState.Modified)
                {
                    entity.EditDateTime = DateTime.Now;
                }

            });

        }

    }
}