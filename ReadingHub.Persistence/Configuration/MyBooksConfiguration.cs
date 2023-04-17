using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingHub.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    internal class MyBooksConfiguration : IEntityTypeConfiguration<MyBooks>
    {
        public void Configure(EntityTypeBuilder<MyBooks> builder)
        {
            builder.Property(e => e.BookStatusId).HasDefaultValue(-1);
        }
    }
}
