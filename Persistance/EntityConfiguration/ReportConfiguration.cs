using Domain.Entities.Autitables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.EntityConfiguration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            var converter = new EnumToStringConverter<ReportType>();
            builder.Property(i => i.Type)
                .HasConversion(converter);

            builder.HasKey(i => i.Id);

            builder.HasMany(r => r.Comments)
                .WithOne(c => c.Report).IsRequired();
        }
    }
}
