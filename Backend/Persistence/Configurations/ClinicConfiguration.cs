using Domain.Clinics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.ToTable("Clinics");

        builder.HasKey(c => c.Id);

        builder.Property(clinic => clinic.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(clinic => clinic.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(clinic => clinic.Code)
            .IsUnique();

        builder.Property(clinic => clinic.PhoneNumber)
            .HasMaxLength(32);

        builder.Property(clinic => clinic.Email)
            .HasMaxLength(256);

        builder.Property(clinic => clinic.Address)
            .HasMaxLength(500);

        builder.Property(clinic => clinic.CreatedAtUtc)
            .IsRequired();

        builder.Property(clinic => clinic.UpdatedAtUtc);

        builder.Property(clinic => clinic.IsActive)
            .IsRequired();
    }
}
