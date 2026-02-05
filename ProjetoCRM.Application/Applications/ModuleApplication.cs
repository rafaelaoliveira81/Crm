using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class ModuleApplication : IModuleApplication
{
    private readonly IModuleRepository _moduleRepository;

    public ModuleApplication(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<int> AddAsync(Module module)
    {
        ValidateModuleInformation(module);

        return await _moduleRepository.AddAsync(module);
    }

    public async Task<Module> GetByIdAsync(int moduleId)
    {
        return await ValidateModuleExistsByIdAsync(moduleId);
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await _moduleRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Module moduleDTO)
    {
        var moduleEntity = await ValidateModuleExistsByIdAsync(moduleDTO.Id);

        ValidateModuleInformation(moduleDTO);

        await _moduleRepository.UpdateAsync(moduleEntity);
    }

    public async Task DeleteAsync(int moduleId)
    {
        var moduleEntity = await ValidateModuleExistsByIdAsync(moduleId);

        await _moduleRepository.DeleteAsync(moduleEntity);
    }

    #region Uteis
    private static void ValidateModuleInformation(Module module)
    {
        if (module == null)
            throw new ArgumentException("Módulo não pode ser vazio.");

        if (string.IsNullOrEmpty(module.Name))
            throw new ArgumentException("O nome do módulo deve ser informado.");
    }

    private async Task<Module> ValidateModuleExistsByIdAsync(int idModuleDTO)
    {
        var moduleEntity = await _moduleRepository.GetByIdAsync(idModuleDTO);

        if (moduleEntity == null)
            throw new KeyNotFoundException("Módulo não localizado.");

        return moduleEntity;
    }
    #endregion
}