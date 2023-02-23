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
    internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasIndex(e => e.Code)
                .IsUnique();
        }
    }
}
