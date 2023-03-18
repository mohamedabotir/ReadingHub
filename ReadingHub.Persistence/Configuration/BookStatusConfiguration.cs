using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    internal class BookStatusConfiguration : IEntityTypeConfiguration<BookStatus>
    {
        public void Configure(EntityTypeBuilder<BookStatus> builder)
        {
            builder.HasIndex(e=>e.StatusName)
                .IsUnique();
        }
    }
}
