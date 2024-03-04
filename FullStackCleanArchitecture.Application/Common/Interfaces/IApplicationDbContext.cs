using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
