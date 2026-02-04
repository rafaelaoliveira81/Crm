using ProjetoCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjetoCRM.Repository.Context;

namespace ProjetoCRM.Repository;

public class PlanRepository : BaseRepository, IPlanRepository
{
    public PlanRepository(ProjetoCRMContext context) : base(context)
    {
    }

    public Task<int> AddAsync(Plan plan)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Plan plan)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Plan>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Plan> GetByIdAsync(int idPlan)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Plan plan)
    {
        throw new NotImplementedException();
    }
}