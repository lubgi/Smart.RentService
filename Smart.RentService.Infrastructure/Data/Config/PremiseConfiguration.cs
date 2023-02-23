using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Infrastructure.Data.Config
{
    internal class PremiseConfiguration : IEntityTypeConfiguration<Premise>
    {
        public void Configure(EntityTypeBuilder<Premise> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(50);

            builder.HasIndex(p => p.Code)
                .IsUnique();
        }
    }
}
