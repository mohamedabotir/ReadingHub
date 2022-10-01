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
    public class ReadingNotesConfiguration : IEntityTypeConfiguration<ReadingNotes>
    {
        public void Configure(EntityTypeBuilder<ReadingNotes> builder)
        {
            builder.Property(x => x.Notes).HasMaxLength(250);
        }
    }
}
