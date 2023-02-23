using Ardalis.Specification;

namespace Smart.RentService.SharedKernel.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}
