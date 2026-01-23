using ProjetoCRM.Domain.Entities;

public interface IUserRepository
{
    Task<int> AddAsync(User user);
    Task<User> GetByIdAsync(int idUser);
    Task<User> GetByEmailAsync(string emailUser);
    Task<IEnumerable<User>> GetAllAsync();
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}