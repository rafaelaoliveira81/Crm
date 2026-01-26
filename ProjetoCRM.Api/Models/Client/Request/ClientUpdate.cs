namespace ProjetoCRM.Api.Models.Request;

public class ClientUpdate
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}