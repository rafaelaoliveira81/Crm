using ProjetoCRM.Domain.Enuns;
namespace ProjetoCRM.Api.Models.Request;

public class AppointmentUpdate
{
    public int ID { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Description { get; set; }
    public int ClientId { get; set; }
    public int SpecialistId { get; set; }
    public int UserId { get; set; }
}