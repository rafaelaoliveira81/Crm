using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class PlanApplication : IPlanApplication
{
    private readonly IPlanRepository _planRepository;

    public PlanApplication(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<int> AddAsync(Plan plan)
    {
        ValidatePlanInformation(plan);

        return await _planRepository.AddAsync(plan);
    }

    public async Task<Plan> GetByIdAsync(int planId)
    {
        return await ValidatePlanExistsByIdAsync(planId);
    }

    public async Task<IEnumerable<Plan>> GetAllAsync()
    {
        return await _planRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Plan planDTO)
    {
        var planEntity = await ValidatePlanExistsByIdAsync(planDTO.Id);

        ValidatePlanInformation(planDTO);

        await _planRepository.UpdateAsync(planEntity);
    }

    public async Task DeleteAsync(int planId)
    {
        var planEntity = await ValidatePlanExistsByIdAsync(planId);

        await _planRepository.DeleteAsync(planEntity);
    }

    #region Uteis
    private static void ValidatePlanInformation(Plan plan)
    {
        if (plan == null)
            throw new ArgumentException("Plano não pode ser vazio.");

        if (string.IsNullOrEmpty(plan.Name))
            throw new ArgumentException("O nome do plano deve ser informado.");

        if (plan.DefaultPrice <= 0)
            throw new ArgumentException("O preço do plano deve ser maior que zero.");
    }

    private async Task<Plan> ValidatePlanExistsByIdAsync(int idPlanDTO)
    {
        var planEntity = await _planRepository.GetByIdAsync(idPlanDTO);

        if (planEntity == null)
            throw new KeyNotFoundException("Plano não localizado.");

        return planEntity;
    }

    #endregion
}