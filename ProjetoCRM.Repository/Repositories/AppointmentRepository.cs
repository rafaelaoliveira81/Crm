using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class AppointmentRepository : BaseRepository, IAppointmentRepository
{
    public AppointmentRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public async Task<int> AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return appointment.ID;
    }

    public async Task<Appointment> GetByIdAsync(int idAppointment)
    {
        return await _context.Appointments.FindAsync(idAppointment);
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }
    public async Task<IEnumerable<Appointment>> GetAllBySpecialistIdAsync(int specialistId)
    {
        return await _context.Appointments
            .Where(a => a.SpecialistId == specialistId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllByClientIdAsync(int clientId)
    {
        return await _context.Appointments
            .Where(a => a.ClientId == clientId)
            .ToListAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }
}