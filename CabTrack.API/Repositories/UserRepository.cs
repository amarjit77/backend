using CabTrack.API.Data;
using CabTrack.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTrack.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByUsernameAsync(string username)
        => await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);

    public async Task<User?> GetByIdAsync(int id)
        => await _dbContext.Users.FindAsync(id);

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}