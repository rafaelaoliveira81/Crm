using ProjetoCRM.Domain.Enuns;

namespace ProjetoCRM.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PlanId { get; set; }
    public Plan Plan { get; set; }
    public AccountStatus Status { get; set; }
    public decimal ContractValue { get; set; }
    public int HealthScore { get; set; }
    public DateTime CreatedAt { get; set; }

}