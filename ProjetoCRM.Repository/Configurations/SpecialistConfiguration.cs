using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Repository.Configurations;

public class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
{
    public void Configure(EntityTypeBuilder<Specialist> builder)
    {
        builder.ToTable("Specialists");
        builder.HasKey(u => u.ID);

        builder.Property(u => u.Name).IsRequired(true).HasMaxLength(150);
        builder.Property(u => u.IsActive).IsRequired(true);
        builder.Property(u => u.CreatedAt).IsRequired(true).ValueGeneratedOnAdd();
        builder.Property(u => u.UpdatedAt);

        builder.HasOne(s => s.User).WithOne(u => u.Specialist).HasForeignKey<Specialist>(s => s.UserId);
    }
}