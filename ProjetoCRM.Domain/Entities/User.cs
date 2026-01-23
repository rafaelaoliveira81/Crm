namespace ProjetoCRM.Domain.Entities;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }

    public User()
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