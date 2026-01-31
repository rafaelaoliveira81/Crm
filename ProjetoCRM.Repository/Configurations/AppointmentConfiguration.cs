using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Repository.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments");
        builder.HasKey(a => a.ID);

        builder.Property(a => a.StartAt).IsRequired(true);
        builder.Property(a => a.EndAt).IsRequired(true);
        builder.Property(a => a.Status).IsRequired(true);
        builder.Property(a => a.Description).HasMaxLength(500);
        builder.Property(a => a.CreatedAt).IsRequired(true).ValueGeneratedOnAdd();
        builder.Property(a => a.UpdatedAt);

        builder.HasOne(a => a.Client)
               .WithMany(c => c.Appointments)
               .HasForeignKey(a => a.ClientId)
               .IsRequired(true);

        builder.HasOne(a => a.Specialist)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SpecialistId)
                .IsRequired(true);
    }
}