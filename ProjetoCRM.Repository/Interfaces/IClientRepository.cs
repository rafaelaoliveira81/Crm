using ProjetoCRM.Domain.Entities;

public interface IClientRepository
{
    Task<int> AddAsync(Client client);
    Task<Client> GetByIdAsync(int idClient);
    Task<Client> GetByNameAsync(string nameClient);
    Task<Client> GetByPhoneNumberAsync(string phoneNumberClient);
    Task<Client> GetByEmailAsync(string emailClient);
    Task<IEnumerable<Client>> GetAllAsync();
    Task UpdateAsync(Client client);
    Task DeleteAsync(Client client);
}