// File: IUserService.cs
using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task CreateUserAsync(UserDTO userDto);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int userId);
    }
}