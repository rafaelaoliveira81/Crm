namespace ProjetoCRM.Domain.Entities;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DefaultPrice { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
