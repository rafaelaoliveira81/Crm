using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Repository.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired(true)
            .HasMaxLength(200);

        builder.Property(a => a.PlanId)
            .IsRequired(true);

        builder.Property(a => a.Status)
            .IsRequired(true);

        builder.Property(a => a.ContractValue)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.HealthScore);

        builder.Property(a => a.CreatedAt)
            .IsRequired(true)
            .ValueGeneratedOnAdd();
    }    
}