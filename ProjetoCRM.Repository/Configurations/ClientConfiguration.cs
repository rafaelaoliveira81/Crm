using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Repository.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.ID);

        builder.Property(c => c.Name).IsRequired(true).HasMaxLength(150);
        builder.Property(c => c.Email).HasMaxLength(254);
        builder.Property(c => c.PhoneNumber).HasMaxLength(20);
        builder.Property(c => c.IsActive).IsRequired(true);
        builder.Property(c => c.CreatedAt).IsRequired(true).ValueGeneratedOnAdd();
        builder.Property(c => c.UpdatedAt);
    }
}