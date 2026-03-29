using CabTrack.API.Domain.Entities;

namespace CabTrack.API.Repositories;

public interface ITripRepository
{
    Task<Trip> AddAsync(Trip trip);
    Task<List<Trip>> GetAllByUserAsync(int userId);
    Task<Trip?> GetByIdAsync(int id, int userId);
    Task UpdateAsync(Trip trip);
    Task DeleteAsync(Trip trip);
    Task SaveChangesAsync();
    Task<List<Trip>> GetTripsInDateRangeAsync(int userId, DateTime start, DateTime end);
}