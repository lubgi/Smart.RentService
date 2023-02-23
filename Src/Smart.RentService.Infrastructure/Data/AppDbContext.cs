using Microsoft.EntityFrameworkCore;
using Smart.RentService.Core.Entities;
using Smart.RentService.Infrastructure.Data.Interceptors;
using System.Reflection;

namespace Smart.RentService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<Equipment> Equipments => Set<Equipment>();
        public DbSet<Premise> Premises => Set<Premise>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }
}
