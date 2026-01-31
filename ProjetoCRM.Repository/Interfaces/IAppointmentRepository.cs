using ProjetoCRM.Domain.Entities;

public interface IAppointmentRepository
{
    Task<int> AddAsync(Appointment appointment);
    Task<Appointment> GetByIdAsync(int idAppointment);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<IEnumerable<Appointment>> GetAllBySpecialistIdAsync(int specialistId);
    Task<IEnumerable<Appointment>> GetAllByClientIdAsync(int clientId);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
}
