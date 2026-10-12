
using Application.Abstractions.Persistence;
using Domain.Clinics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;

namespace Persistence;

public sealed class MediCoreDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>,
      IApplicationDbContext
{
    public MediCoreDbContext(
        DbContextOptions<MediCoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Clinic> Clinics => Set<Clinic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MediCoreDbContext).Assembly);
    }
}
