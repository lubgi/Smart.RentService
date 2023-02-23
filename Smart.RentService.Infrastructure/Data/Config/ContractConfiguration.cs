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
    internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasOne(c => c.Premise)
                .WithMany(p => p.Contracts)
                .HasForeignKey(c => c.PremiseId);

            builder.HasOne(c => c.Equipment)
                .WithMany(e => e.Contracts)
                .HasForeignKey(c => c.EquipmentId);
        }
    }
}
