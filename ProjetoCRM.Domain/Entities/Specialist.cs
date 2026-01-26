namespace ProjetoCRM.Domain.Entities;

public class Specialist : BaseEntity
{
    public int ID { get; set; }
    public string Name { get; set; }
    public User User { get; set; }
    public int? UserId { get; set; }
    public bool IsActive { get; set; }

    public Specialist()
    {
        IsActive = true;
    }

    public void Deletar()
    {
        IsActive = false;
    }

    public void Restore()
    {
        IsActive = true;
    }
}