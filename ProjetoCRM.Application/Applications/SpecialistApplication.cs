using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class SpecialistApplication : ISpecialistApplication
{
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IUserApplication _userApplication;

    public SpecialistApplication(ISpecialistRepository specialistRepository, IUserApplication userApplication)
    {
        _specialistRepository = specialistRepository;
        _userApplication = userApplication;
    }

    public async Task<int> AddAsync(Specialist specialistDTO)
    {
        ValidateSpecialistInformation(specialistDTO);

        var specislistEntity = await _specialistRepository.GetByNameAsync(specialistDTO.Name);
        if (specislistEntity != null)
            throw new ArgumentException("Já existe especialista com o nome informado.");

        if (specialistDTO.UserId > 0)
        {
            await GetUserByIdAsync(specialistDTO);

            return await _specialistRepository.AddAsync(specialistDTO);
        }

        specialistDTO.UserId = null;

        return await _specialistRepository.AddAsync(specialistDTO);
    }

    public async Task DeleteAsync(int idSpecialistDTO)
    {
        var specislistEntity = await ValidateSpecialistExistsByIdAsync(idSpecialistDTO);

        // IsActive = false para manter o registro no banco de dados. 
        specislistEntity.Deletar();

        await _specialistRepository.UpdateAsync(specislistEntity);
    }

    public async Task<IEnumerable<Specialist>> GetAllAsync()
    {
        return await _specialistRepository.GetAllAsync();
    }

    public async Task<Specialist> GetByIdAsync(int idSpecialistDTO)
    {
        return await ValidateSpecialistExistsByIdAsync(idSpecialistDTO);
    }

    public async Task<Specialist> GetByNameAsync(string nameSpecialistDTO)
    {
        if (string.IsNullOrWhiteSpace(nameSpecialistDTO))
            throw new ArgumentException("Nome não pode ser vazio.");

        var specislistEntity = await _specialistRepository.GetByNameAsync(nameSpecialistDTO);

        if (specislistEntity == null)
            throw new KeyNotFoundException("Usuário não encontrado");

        return specislistEntity;
    }

    public async Task RestoreAsync(int idSpecialistDTO)
    {
        var specislistEntity = await ValidateSpecialistExistsByIdAsync(idSpecialistDTO);

        specislistEntity.Restore();

        await _specialistRepository.UpdateAsync(specislistEntity);
    }

    public async Task UpdateAsync(Specialist specialistDTO)
    {
        ValidateSpecialistInformation(specialistDTO);

        var specialistEntity = await ValidateSpecialistExistsByIdAsync(specialistDTO.ID);

        var specialist = await _specialistRepository.GetByNameAsync(specialistDTO.Name);
        if (specialist != null && specialist.ID != specialistDTO.ID)
            throw new ArgumentException("Já existe Especialista com o nome informado.");

        if (specialistDTO.UserId == 0)
            specialistDTO.UserId = null;

        specialistEntity.Name = specialistDTO.Name;
        specialistEntity.UserId = specialistDTO.UserId;

        await _specialistRepository.UpdateAsync(specialistDTO);
    }

    #region Úteis
    private static void ValidateSpecialistInformation(Specialist specialistDTO)
    {
        if (specialistDTO == null)
            throw new ArgumentException("Especialista não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(specialistDTO.Name))
            throw new ArgumentException("Nome não pode ser vazio.");
    }

    private async Task<Specialist> ValidateSpecialistExistsByIdAsync(int idSpecialistDTO)
    {
        var specislistEntity = await _specialistRepository.GetByIdAsync(idSpecialistDTO);

        if (specislistEntity == null)
            throw new KeyNotFoundException("Especialista não localizado.");

        return specislistEntity;
    }

    private async Task GetUserByIdAsync(Specialist specialistDTO)
    {
        var user = await _userApplication.GetByIdAsync((int)specialistDTO.UserId);
        if (user == null)
            throw new KeyNotFoundException("Usuário não localizado.");
    }
    #endregion
}