using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Configurations;

namespace ProjetoCRM.Repository.Context;

public class ProjetoCRMContext : DbContext
{
    public ProjetoCRMContext(DbContextOptions<ProjetoCRMContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Specialist> Specialists { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialistConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        // Pega TODAS as entidades que herdam de BaseEntity
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            // Se a entidade está sendo criada
            if (entry.State == EntityState.Added)
            {
                // CreatedAt fica por conta do banco (DEFAULT GETDATE)
                entry.Entity.UpdatedAt = null;
            }

            // Se a entidade está sendo atualizada
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }

}
