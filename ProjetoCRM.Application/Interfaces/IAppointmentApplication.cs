using ProjetoCRM.Domain.Entities;

public interface IAppointmentApplication
{
    Task<int> AddAsync(Appointment appointment);
    Task<Appointment> GetByIdAsync(int appointmentId);
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<IEnumerable<Appointment>> GetAllBySpecialistIdAsync(int specialistId);
    Task<IEnumerable<Appointment>> GetAllByClientIdAsync(int clientId);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int appointmentId);
}