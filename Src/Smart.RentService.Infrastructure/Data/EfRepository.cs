using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
