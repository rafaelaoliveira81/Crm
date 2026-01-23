using ProjetoCRM.Domain.Entities;

public interface IUserApplication
{
    Task<int> AddAsync(User userDTO);
    Task<User> GetByIdAsync(int idUserDTO);
    Task<User> GetByEmailAsync(string emailUserDTO);
    Task<IEnumerable<User>> GetAllAsync();
    Task UpdateAsync(User userDTO);
    Task DeleteAsync(int idUserDTO);
    Task RestoreAsync(int idUserDTO);
    Task UpdatePasswordAsync(User userDto, string currentPassword);
}