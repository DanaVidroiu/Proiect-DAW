using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Services
{
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        return await _context.Users
                             .Select(user => new UserDTO
                             {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Email = user.Email,
                                 IsProfessor = user.IsProfessor,
                                 UserName = user.UserName
                             })
                             .ToListAsync();
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
                                  .Where(u => u.Id == id)
                                  .Select(u => new UserDTO
                                  {
                                      Id = u.Id,
                                      Name = u.Name,
                                      Email = u.Email,
                                      IsProfessor = u.IsProfessor,
                                      UserName = u.UserName
                                  })
                                  .FirstOrDefaultAsync();
        return user;
    }

    public async Task CreateUserAsync(UserDTO userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            IsProfessor = userDto.IsProfessor,
            UserName = userDto.UserName
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserDTO userDto)
    {
        var user = await _context.Users.FindAsync(userDto.Id);
        if (user != null)
        {
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.IsProfessor = userDto.IsProfessor;
            user.UserName = userDto.UserName;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
}
