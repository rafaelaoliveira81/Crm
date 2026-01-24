using ProjetoCRM.Domain.Entities;

namespace ProjetoCRM.Application;

public class UserApplication : IUserApplication
{
    private readonly IUserRepository _userRepository;

    public UserApplication(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> AddAsync(User user)
    {
        // Valida os campos obrigatórios e regras de negócio do usuário
        ValidateUserInformation(user);

        // Valida se a senha foi preenchida
        if (string.IsNullOrWhiteSpace(user.Password))
            throw new ArgumentException("A senha do usuário deve ser informada.");

        // Verifica se já existe outro usuário utilizando o e-mail informado
        var userEntity = await _userRepository.GetByEmailAsync(user.Email);
        if (userEntity != null)
            throw new ArgumentException("Já existe usuário com o e-mail informado.");

        return await _userRepository.AddAsync(user);
    }

    public async Task<User> GetByIdAsync(int idUserDTO)
    {
        return await ValidateUserExistsByIdAsync(idUserDTO);
    }

    public async Task<User> GetByEmailAsync(string emailUserDTO)
    {
        var userEntity = await _userRepository.GetByEmailAsync(emailUserDTO);

        if (userEntity == null)
            throw new KeyNotFoundException("Usuário não localizado.");

        return userEntity;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task UpdateAsync(User userDTO)
    {
        // Garante que o usuário existe pelo ID; caso não exista, lança uma exceção
        var userEntity = await ValidateUserExistsByIdAsync(userDTO.ID);

        // Valida os campos obrigatórios e regras de negócio do usuário
        ValidateUserInformation(userDTO);

        // Verifica se já existe outro usuário utilizando o e-mail informado
        var user = await _userRepository.GetByEmailAsync(userDTO.Email);

        // Caso exista um usuário com o mesmo e-mail e não seja o próprio usuário que está sendo atualizado, lança exceção
        if (user != null && user.ID != userEntity.ID)
            throw new ArgumentException("Já existe um usuário com o e-mail informado.");

        // Atualiza os dados do usuário existente
        userEntity.Name = userDTO.Name;
        userEntity.Email = userDTO.Email;

        // Salva as alterações no banco de dados
        await _userRepository.UpdateAsync(userEntity);
    }

    public async Task DeleteAsync(int idUserDTO)
    {
        var userEntity = await ValidateUserExistsByIdAsync(idUserDTO);

        userEntity.Deletar();

        await _userRepository.UpdateAsync(userEntity);
    }

    public async Task RestoreAsync(int idUserDTO)
    {
        var userEntity = await ValidateUserExistsByIdAsync(idUserDTO);

        userEntity.Restore();

        await _userRepository.UpdateAsync(userEntity);
    }

    public async Task UpdatePasswordAsync(User userDTO, string currentPassword)
    {
        var userEntity = await _userRepository.GetByIdAsync(userDTO.ID);

        if (string.IsNullOrWhiteSpace(currentPassword))
            throw new ArgumentException("A senha atual deve ser informada.");

        if (userEntity.Password != currentPassword)
            throw new UnauthorizedAccessException("Senha atual incorreta.");

        userEntity.Password = userDTO.Password;

        await _userRepository.UpdateAsync(userEntity);
    }

    #region Uteis
    private static void ValidateUserInformation(User user)
    {
        if (user == null)
            throw new ArgumentException("Usuário não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(user.Name))
            throw new ArgumentException("O nome do usuário deve ser informado.");

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("O e-mail do usuário deve ser informado.");
    }

    private async Task<User> ValidateUserExistsByIdAsync(int idUserDTO)
    {
        var userEntity = await _userRepository.GetByIdAsync(idUserDTO);

        if (userEntity == null)
            throw new KeyNotFoundException("Usuário não localizado.");

        return userEntity;
    }
    #endregion
}