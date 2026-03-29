using CabTrack.API.DTOs.Trips;

namespace CabTrack.API.Services;

public interface ITripService
{
    Task<TripDto> CreateTripAsync(int userId, TripCreateDto dto);
    Task<IEnumerable<TripDto>> GetTripsAsync(int userId);
    Task<TripDto> GetTripByIdAsync(int userId, int id);
    Task UpdateTripAsync(int userId, int id, TripUpdateDto dto);
    Task DeleteTripAsync(int userId, int id);
    Task<decimal> GetReportAsync(int userId, DateTime start, DateTime end);
}