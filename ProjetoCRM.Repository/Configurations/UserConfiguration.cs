using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.ID);

        builder.Property(u => u.Name).IsRequired(true).HasMaxLength(150);
        builder.Property(u => u.Email).IsRequired(true).HasMaxLength(254);
        builder.Property(u => u.Password).IsRequired(true).HasMaxLength(255);
        builder.Property(u => u.IsActive).IsRequired(true);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}