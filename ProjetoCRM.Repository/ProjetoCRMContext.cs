using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Configurations;

namespace ProjetoCRM.Repository.Context;

public class ProjetoCRMContext : DbContext
{
    // private readonly DbContextOptions _options;

    // public ProjetoCRMContext(DbContextOptions options)
    // {
    //     _options = options;
    // }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=RAFAELA_OLI\\SQLEXPRESS;Database=ProjetoCRM;Trusted_Connection=True;TrustServerCertificate=True;");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}