using ProjetoCRM.Domain.Enuns;

namespace ProjetoCRM.Domain.Entities;

public class Appointment : BaseEntity
{
    public int ID { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Description { get; set; }
    public int? ClientId { get; set; }
    public Client Client { get; set; }
    public int? SpecialistId { get; set; }
    public Specialist Specialist { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; }
}