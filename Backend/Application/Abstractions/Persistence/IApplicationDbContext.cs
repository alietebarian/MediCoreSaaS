using Domain.Clinics;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<Clinic> Clinics { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
