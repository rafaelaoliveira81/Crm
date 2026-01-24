namespace ProjetoCRM.Api.Models.Request;

public class UserUpdatePassword
{
    public int IdUser { get; set; }
    public string NewPassword { get; set; }
    public string CurrentPassword { get; set; }
}