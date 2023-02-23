using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Smart.RentService.Infrastructure.Interfaces;
using Smart.RentService.SharedKernel;

namespace Smart.RentService.Infrastructure.Data.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IDateTimeProvider _dateTime;

        public AuditableEntitySaveChangesInterceptor(IDateTimeProvider dateTime)
        {
            _dateTime = dateTime;
        }


        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntityBase>())
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.Created = _dateTime.Now;
                }

                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    entry.Entity.LastModified = _dateTime.Now;
                }
            }
        }
    }
}
