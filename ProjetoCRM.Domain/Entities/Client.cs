namespace ProjetoCRM.Domain.Entities;

public class Client : BaseEntity
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    public Client()
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