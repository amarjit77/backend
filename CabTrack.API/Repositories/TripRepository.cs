using CabTrack.API.Data;
using CabTrack.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTrack.API.Repositories;

public class TripRepository : ITripRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TripRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Trip> AddAsync(Trip trip)
    {
        var entity = (await _dbContext.Trips.AddAsync(trip)).Entity;
        return entity;
    }

    public async Task<List<Trip>> GetAllByUserAsync(int userId)
    {
        return await _dbContext.Trips
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.PickupTime)
            .ToListAsync();
    }

    public async Task<Trip?> GetByIdAsync(int id, int userId)
    {
        return await _dbContext.Trips.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public Task UpdateAsync(Trip trip)
    {
        _dbContext.Trips.Update(trip);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Trip trip)
    {
        _dbContext.Trips.Remove(trip);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Trip>> GetTripsInDateRangeAsync(int userId, DateTime start, DateTime end)
    {
        return await _dbContext.Trips
            .Where(t => t.UserId == userId && t.PickupTime >= start && t.PickupTime <= end)
            .ToListAsync();
    }
}