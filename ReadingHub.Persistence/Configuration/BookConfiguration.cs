using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingHub.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingHub.Persistence.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
       

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(e => e.BookName).HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(300);
            builder.HasIndex(e => e.BookName).IsUnique(true);
            builder.HasMany(e => e.Comments).WithOne(e => e.Book);
        }
    }
}
