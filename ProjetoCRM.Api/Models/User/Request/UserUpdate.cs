namespace ProjetoCRM.Api.Models.Request;

public class UserUpdate
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}