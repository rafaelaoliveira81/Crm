using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class ClientApplication : IClientApplication
{
    private readonly IClientRepository _clientRepository;

    public ClientApplication(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<int> AddAsync(Client client)
    {
        // Valida os campos obrigatórios e regras de negócio do cliente
        ValidateClientInformation(client);

        // Verifica se já existe outro cliente utilizando o e-mail informado
        var clientEntity = await _clientRepository.GetByEmailAsync(client.Email);
        if (clientEntity != null)
            throw new ArgumentException("Já existe cliente com o e-mail informado.");

        return await _clientRepository.AddAsync(client);
    }

    public async Task<Client> GetByIdAsync(int idClientDTO)
    {
        return await ValidateClientExistsByIdAsync(idClientDTO);
    }

    public async Task<Client> GetByEmailAsync(string emailClientDTO)
    {
        if (string.IsNullOrWhiteSpace(emailClientDTO))
            throw new ArgumentException("Email não pode ser vazio");

        var clientEntity = await _clientRepository.GetByEmailAsync(emailClientDTO);

        if (clientEntity == null)
            throw new KeyNotFoundException("Cliente não localizado.");

        return clientEntity;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task UpdateAsync(Client clientDTO)
    {
        // Garante que o cliente existe pelo ID; caso não exista, lança uma exceção
        var clientEntity = await ValidateClientExistsByIdAsync(clientDTO.ID);

        // Valida os campos obrigatórios e regras de negócio do cliente
        ValidateClientInformation(clientDTO);

        // Verifica se já existe outro cliente utilizando o e-mail informado
        var client = await _clientRepository.GetByEmailAsync(clientDTO.Email);

        // Caso exista um cliente com o mesmo e-mail e não seja o próprio cliente que está sendo atualizado, lança exceção
        if (client != null && client.ID != clientEntity.ID)
            throw new ArgumentException("Já existe um cliente com o e-mail informado.");
        // Atualiza os dados do cliente existente
        clientEntity.Name = clientDTO.Name;
        clientEntity.Email = clientDTO.Email;

        // Salva as alterações no banco de dados
        await _clientRepository.UpdateAsync(clientEntity);
    }

    public async Task DeleteAsync(int idClientDTO)
    {
        var clientEntity = await ValidateClientExistsByIdAsync(idClientDTO);

        clientEntity.Deletar();
        await _clientRepository.UpdateAsync(clientEntity);
    }

    public async Task RestoreAsync(int idClientDTO)
    {
        var clientEntity = await ValidateClientExistsByIdAsync(idClientDTO);

        clientEntity.Restore();

        await _clientRepository.UpdateAsync(clientEntity);
    }

    public async Task<Client> GetByNameAsync(string nameClientDTO)
    {
        if (string.IsNullOrWhiteSpace(nameClientDTO))
            throw new ArgumentException("Nome não pode ser vazio");

        var clientEntity = await _clientRepository.GetByNameAsync(nameClientDTO);
        if (clientEntity == null)
            throw new KeyNotFoundException("Cliente não localizado.");

        return clientEntity;
    }

    public Task<Client> GetByPhoneNumberAsync(string phoneNumberClientDTO)
    {
        if (string.IsNullOrWhiteSpace(phoneNumberClientDTO))
            throw new ArgumentException("Número de telefone não pode ser vazio");

        var clientEntity = _clientRepository.GetByPhoneNumberAsync(phoneNumberClientDTO);
        if (clientEntity == null)
            throw new KeyNotFoundException("Cliente não localizado.");

        return clientEntity;
    }

    #region Uteis
    private static void ValidateClientInformation(Client client)
    {
        if (client == null)
            throw new ArgumentException("Cliente não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(client.Name))
            throw new ArgumentException("O nome do cliente deve ser informado.");
    }

    private async Task<Client> ValidateClientExistsByIdAsync(int idClientDTO)
    {
        var clientEntity = await _clientRepository.GetByIdAsync(idClientDTO);

        if (clientEntity == null)
            throw new KeyNotFoundException("Cliente não localizado.");

        return clientEntity;
    }
    #endregion
}