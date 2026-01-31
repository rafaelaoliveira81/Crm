using ProjetoCRM.Domain.Entities;
using ProjetoCRM.Domain.Enuns;

namespace ProjetoCRM.Application;

public class AppointmentApplication : IAppointmentApplication
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IClientApplication _clientApplication;
    private readonly ISpecialistApplication _specialistApplication;

    public AppointmentApplication(IAppointmentRepository appointmentRepository, IClientApplication clientApplication, ISpecialistApplication specialistApplication)
    {
        _appointmentRepository = appointmentRepository;
        _clientApplication = clientApplication;
        _specialistApplication = specialistApplication;
    }

    public async Task<int> AddAsync(Appointment appointment)
    {
        ValidateAppointmentInformation(appointment);

        if (appointment.SpecialistId > 0)
            await GetSpecialistByIdAsync((int)appointment.SpecialistId);
        else
            appointment.SpecialistId = null;

        if (appointment.ClientId > 0)
            await GetClientByIdAsync((int)appointment.ClientId);
        else
            appointment.ClientId = null;

        if (appointment.UserId > 0)
            await GetUserByIdAsync((int)appointment.UserId);
        else
            appointment.UserId = null;

        return await _appointmentRepository.AddAsync(appointment);
    }

    public async Task<Appointment> GetByIdAsync(int appointmentId)
    {
        return await ValidateAppointmentExistsByIdAsync(appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _appointmentRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllBySpecialistIdAsync(int specialistId)
    {
        await GetSpecialistByIdAsync(specialistId);

        return await _appointmentRepository.GetAllBySpecialistIdAsync(specialistId);
    }

    public async Task<IEnumerable<Appointment>> GetAllByClientIdAsync(int clientId)
    {
        await GetClientByIdAsync(clientId);

        return await _appointmentRepository.GetAllByClientIdAsync(clientId);
    }

    public async Task UpdateAsync(Appointment appointmentDTO)
    {
        var appointmentEntity = await ValidateAppointmentExistsByIdAsync(appointmentDTO.ID);

        ValidateAppointmentInformation(appointmentDTO);

        if (appointmentDTO.SpecialistId > 0)
        {
            await GetSpecialistByIdAsync((int)appointmentDTO.SpecialistId);
            appointmentEntity.SpecialistId = appointmentDTO.SpecialistId;
        }

        if (appointmentDTO.ClientId > 0)
        {
            await GetClientByIdAsync((int)appointmentDTO.ClientId);
            appointmentEntity.ClientId = appointmentDTO.ClientId;
        }

        if (appointmentDTO.UserId > 0)
        {
            await GetUserByIdAsync((int)appointmentDTO.UserId);
            appointmentEntity.UserId = appointmentDTO.UserId;
        }

        appointmentEntity.StartAt = appointmentDTO.StartAt;
        appointmentEntity.EndAt = appointmentDTO.EndAt;
        appointmentEntity.Status = appointmentDTO.Status;

        await _appointmentRepository.UpdateAsync(appointmentEntity);
    }

    public async Task DeleteAsync(int appointmentId)
    {
        var appointmentEntity = await ValidateAppointmentExistsByIdAsync(appointmentId);

        await _appointmentRepository.DeleteAsync(appointmentEntity);
    }

    #region Uteis
    private static void ValidateAppointmentInformation(Appointment appointment)
    {
        if (appointment == null)
            throw new ArgumentException("Agendamento não pode ser vazio.");

        if (appointment.StartAt == default)
            throw new ArgumentException("A data de início do agendamento deve ser informada.");

        if (appointment.EndAt == default)
            throw new ArgumentException("A data de término do agendamento deve ser informada.");

        if (appointment.EndAt <= appointment.StartAt)
            throw new ArgumentException("A data de término do agendamento deve ser maior que a data de início.");

        if (!Enum.IsDefined(typeof(AppointmentStatus), appointment.Status))
            throw new ArgumentException("O status do agendamento deve ser informado.");
    }

    private async Task<Appointment> ValidateAppointmentExistsByIdAsync(int idAppointmentDTO)
    {
        var appointmentEntity = await _appointmentRepository.GetByIdAsync(idAppointmentDTO);

        if (appointmentEntity == null)
            throw new KeyNotFoundException("Agendamento não localizado.");

        return appointmentEntity;
    }

    private async Task GetSpecialistByIdAsync(int specialistId)
    {
        var specialist = await _specialistApplication.GetByIdAsync(specialistId);
        if (specialist == null)
            throw new KeyNotFoundException("Especialista não localizado.");
    }

    private async Task GetClientByIdAsync(int clientId)
    {
        var client = await _clientApplication.GetByIdAsync(clientId);
        if (client == null)
            throw new KeyNotFoundException("Cliente não localizado.");
    }

    private async Task GetUserByIdAsync(int userId)
    {
        var user = await _clientApplication.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("Usuário não localizado.");
    }
    #endregion
}