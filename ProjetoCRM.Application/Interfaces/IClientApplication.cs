using ProjetoCRM.Domain.Entities;

public interface IClientApplication
{
    Task<int> AddAsync(Client clientDTO);
    Task<Client> GetByIdAsync(int idClientDTO);
    Task<Client> GetByNameAsync(string nameClientDTO);
    Task<Client> GetByPhoneNumberAsync(string phoneNumberClientDTO);
    Task<Client> GetByEmailAsync(string emailClientDTO);
    Task<IEnumerable<Client>> GetAllAsync();
    Task UpdateAsync(Client clientDTO);
    Task DeleteAsync(int idClientDTO);
    Task RestoreAsync(int idClientDTO);
}