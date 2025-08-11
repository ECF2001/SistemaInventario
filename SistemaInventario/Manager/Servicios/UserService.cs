using Dapper;
using DreamInCode.Manager.DTO;
using DreamInCode.Manager.Repository;
using Manager.Servicios;
using System.Data.SqlClient;


namespace DreamInCode.Manager.Services
{
    public interface IUserService : ICrud<UsersDTO>
    {

    }
    // Implementación correcta de la interfaz IProveedorService
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // Constructor que recibe IProveedorRepository
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Implementación de GetAllAsync
        public async Task<IEnumerable<UsersDTO>> GetAllAsync()
        {
            return await _userRepository.ListAsync<UsersDTO>();
        }

        // Implementación de GetByIdAsync
        public async Task<UsersDTO?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync<UsersDTO>(id);
        }

        // Implementación de CreateAsync
        public async Task<UsersDTO> CreateAsync(UsersDTO entity)
        {

            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

            return await _userRepository.AddAsync(entity);
        }

        // Implementación de UpdateAsync
        public async Task UpdateAsync(UsersDTO entity)
        {
            await _userRepository.UpdateAsync(entity);
        }

        // Implementación de DeleteAsync
        public async Task DeleteAsync(UsersDTO entity)
        {
            await _userRepository.DeleteAsync(entity);
        }


    }
}


